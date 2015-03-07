using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;
using Telerik.Web.UI;
using BoProductMaster;
using BoOps;
using BOAssociates;
using System.Configuration;
using VoOps;
using BoWerpAdmin;
using VoCustomerPortfolio;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VOAssociates;
using BOAssociates;
using iTextSharp.text.pdf;
using System.IO;
using BoCommon;
using System.Transactions;


namespace WealthERP.OPS
{
    public partial class FixedIncomeOrderEntry : System.Web.UI.UserControl
    {
        string imgPath = "";
        string TargetPath = "";
        string imageUploadPath = "";
        CustomerProofUploadsVO CPUVo = new CustomerProofUploadsVO();

        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;
        CustomerVo customerVo = new CustomerVo();
        //RMVo rmVo = new RMVo();
        //AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        DataSet dsCustomerProof = new DataSet();
        CustomerBo customerBo = new CustomerBo();
        //CustomerVo customerVo = new CustomerVo();
        int custBankAccId;
        int customerId;
        //AdvisorVo adviserVo = new AdvisorVo();
        string strGuid = string.Empty;
        RepositoryBo repoBo;
        float fStorageBalance;
        float fMaxStorage;
        string linkAction = "";
        DataRow drCustomerAssociates;
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociates = new DataTable();
        DataTable dtCustomerAssociatesRaw = new DataTable();
        //------------------
        //CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        //CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        UserVo userVo;
        RMVo rmVo = new RMVo();
        FIOrderVo fiorderVo = new FIOrderVo();
        FIOrderBo fiorderBo = new FIOrderBo();
        BoDematAccount boDematAccount = new BoDematAccount();

        int orderId;
        int orderNumber = 0;
        int customerid;
        string defaultInterestRate;
        string Action;
        int ReqCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            userVo = (UserVo)Session[SessionContents.UserVo];
            repoBo = new RepositoryBo();
            //  DifferenceBetDates();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];

            if (Session["customerid"] != null)
            {
                customerid = Convert.ToInt32(Session["customerid"]);
                GetcustomerDetails();

                //LoadNominees();
                //GetCustomerAssociates(customerid);

            }
            if (!IsPostBack)
            {

                FICategory();
                SetEnabilityControls();
                FIScheme(advisorVo.advisorId, "0");

                GetFIModeOfHolding();
                BindProofTypeDP();


                if (Request.QueryString["fiaction"] != null)
                {
                    ReqCount = 1;
                    Action = Request.QueryString["fiaction"].ToString();
                    if (Action == "View")
                    {
                        SetControls(false);

                    }
                    else if (Action == "Edit")
                    {
                        SetControls(true);

                    }
                    // this.Request.QueryString["fiaction"];
                    //  Request.QueryString.Remove("fiaction");
                    //      Request.QueryString.Clear();

                }
            }


        }



        protected void GetcustomerDetails()
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            PortfolioBo portfolioBo = new PortfolioBo();

            //customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(int.Parse(txtCustomerId.Value));
            //customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            //Session["customerVo"] = customerVo;
            //lblGetBranch.Text = customerVo.BranchName;
            //lblgetPan.Text = customerVo.PANNum;
            //hdnCustomerId.Value = txtCustomerId.Value;
            // hdnPortfolioId.Value = customerPortfolioVo.PortfolioId.ToString();
            //customerId = int.Parse(txtCustomerId.Value);
            //if (ddlsearch.SelectedItem.Value == "2")
            //    lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            //BindBank();
            //BindISAList();
            //BindCustomerNCDIssueList();
            //BindJointHolderNominee();
            //GetFIModeOfHolding();
            //BindDepositoryType();
            //Panel1.Visible = true;

        }

        protected void BindDepositoryType()
        {
            DataTable DsDepositoryNames = new DataTable();
            // DsDepositoryNames = boDematAccount.GetDepositoryName();
            //ddlDepositoryName.DataSource = DsDepositoryNames;
            //if (DsDepositoryNames.Rows.Count > 0)
            //{
            //    ddlDepositoryName.DataTextField = "WCMV_Code";
            //    ddlDepositoryName.DataValueField = "WCMV_Code";
            //    ddlDepositoryName.DataBind();
            //}
            //ddlDepositoryName.Items.Insert(0, new ListItem("Select", "Select"));

        }
        public void SetControlsEnablity()
        {
            ddlModeofHOldingFI.Enabled = true;
            txtOrderDate.Enabled = true;
            txtApplicationDate.Enabled = true;
            lblGetOrderNo.Enabled = true;
            txtApplicationNumber.Enabled = true;
            txtSeries.Enabled = true;
            ddlTranstype.Enabled = true;
            ddlCategory.Enabled = true;
            ddlIssuer.Enabled = true;
            txtExistDepositreceiptno.Enabled = true;
            txtRenAmt.Enabled = true;
            txtMaturDate.Enabled = true;
            txtMatAmt.Enabled = true;
            ddlScheme.Enabled = true;
            ddlSeries.Enabled = true;
            txtPayAmt.Enabled = true;
            ddlSchemeOption.Enabled = true;
            txtExistDepositreceiptno.Enabled = true;
            ddlFrequency.Enabled = true;
        }

        private void SetControls(bool Val)
        {
            ddlModeofHOldingFI.Enabled = Val;
            txtOrderDate.Enabled = Val;
            txtApplicationDate.Enabled = Val;
            lblGetOrderNo.Enabled = Val;
            txtApplicationNumber.Enabled = Val;
            txtSeries.Enabled = Val;
            ddlTranstype.Enabled = Val;
            ddlCategory.Enabled = Val;
            ddlIssuer.Enabled = Val;
            txtExistDepositreceiptno.Enabled = Val;
            txtRenAmt.Enabled = Val;
            txtMaturDate.Enabled = Val;
            txtMatAmt.Enabled = Val;
            ddlScheme.Enabled = Val;
            ddlSeries.Enabled = Val;
            txtPayAmt.Enabled = Val;
            ddlSchemeOption.Enabled = Val;
            txtExistDepositreceiptno.Enabled = Val;
            ddlFrequency.Enabled = Val;

            OrderVo orderVo = new OrderVo();
            if (Session["orderVo"] != null && Session["fiorderVo"] != null)
            {
                orderVo = (OrderVo)Session["orderVo"];
                fiorderVo = (FIOrderVo)Session["fiorderVo"];
            }
            lblGetOrderNo.Text = fiorderVo.OrderNumber.ToString();
            ddlCategory.SelectedValue = fiorderVo.AssetInstrumentCategoryCode;
            ddlCategory_SelectedIndexChanged(this, null);

            ddlIssuer.SelectedValue = fiorderVo.IssuerId;
            ddlIssuer_SelectedIndexChanged(this, null);

            ddlScheme.SelectedValue = fiorderVo.SchemeId.ToString();
            ddlScheme_SelectedIndexChanged(this, null);



            ddlSeries.SelectedValue = fiorderVo.SeriesId.ToString();
            ddlSeries_SelectedIndexChanged(this, null);



            //   lblGetOrderNo.Text = orderVo.OrderNumber.ToString();//= Convert.ToInt32();
            if (fiorderVo.Privilidge == "Seniorcitizens")
            {
                ChkSeniorcitizens.Checked = true;

            }
            else if (fiorderVo.Privilidge == "Widow")
            {
                ChkWidow.Checked = true;
            }
            else if (fiorderVo.Privilidge == "ArmedForcePersonnel")
            {
                ChkArmedForcePersonnel.Checked = true;
            }
            else if (fiorderVo.Privilidge == "Existingrelationship")
            {
                CHKExistingrelationship.Checked = true;

            }

            if (fiorderVo.Depositpayableto == "Firstholder")
            {
                ChkFirstholder.Checked = true;
            }
            else if (fiorderVo.Depositpayableto == "Either or survivor")
            {
                ChkEORS.Checked = true;

            }

            if (Convert.ToDateTime(orderVo.OrderDate) != DateTime.MinValue)
                txtOrderDate.SelectedDate = orderVo.OrderDate;
            //else
            //    txtOrderDate.SelectedDate = DateTime.MinValue;

            //= Convert.ToDateTime();
            if (Convert.ToDateTime(orderVo.ApplicationReceivedDate) != DateTime.MinValue)
                txtApplicationDate.SelectedDate = orderVo.OrderDate;
            //else
            //    txtApplicationDate.SelectedDate = DateTime.MinValue;


            //   txtApplicationDate.FocusedDate =  orderVo.ApplicationReceivedDate; //= Convert.ToDateTime();
            txtApplicationNumber.Text = orderVo.ApplicationNumber;//=;

            //  txtSeries.Text = fiorderVo.SeriesDetails;
            ddlTranstype.SelectedValue = fiorderVo.TransactionType;
            ddlTranstype_SelectedIndexChanged(this, null);

            ddlModeofHOldingFI.SelectedValue = fiorderVo.ModeOfHolding;

            txtExistDepositreceiptno.Text = fiorderVo.ExisitingDepositreceiptno;

            if (!string.IsNullOrEmpty(fiorderVo.RenewalAmount.ToString()))
                txtRenAmt.Text = fiorderVo.RenewalAmount.ToString();
            else
                txtRenAmt.Text = "0";


            if (Convert.ToDateTime(fiorderVo.MaturityDate) != DateTime.MinValue)
                txtMaturDate.SelectedDate = fiorderVo.MaturityDate;
            //else
            //    txtMaturDate.SelectedDate = DateTime.MinValue;

            // txtMaturDate.FocusedDate =fiorderVo.MaturityDate ;

            // if (!string.IsNullOrEmpty(fiorderVo.MaturityAmount))
            txtMatAmt.Text = fiorderVo.MaturityAmount.ToString();
            //  else
            //    txtMatAmt.Text = " 0";



            //if (!string.IsNullOrEmpty(txtPayAmt.Text))
            txtPayAmt.Text = Convert.ToDouble(fiorderVo.AmountPayable).ToString();
            //  else
            //  txtPayAmt.Text = "0";

            //ddlModeofHOlding.SelectedValue = "0";// fiorderVo.ModeOfHolding;

            ddlSchemeOption.SelectedValue = fiorderVo.Schemeoption;
            if (!string.IsNullOrEmpty(fiorderVo.ExisitingDepositreceiptno))
                txtExistDepositreceiptno.Text = fiorderVo.ExisitingDepositreceiptno;
            else
                txtExistDepositreceiptno.Text = "";

            ddlFrequency.SelectedValue = fiorderVo.Frequency;
            ddlFrequency_SelectedIndexChanged(this, null);
            // fiorderVo.Privilidge = "";


            //if (rbtnNo.Checked)
            //{
            //    customerAccountsVo.IsJointHolding = 0;
            //}
            //if (rbtnYes.Checked)
            //{
            //    customerAccountsVo.IsJointHolding = 1;
            //}




        }

        protected void txtMaturDate_DateChanged(object sender, EventArgs e)
        {
            //int minTenure = 0;
            //int maxTenure = 0;
            //if (ddlSeries.SelectedValue != "0")
            //    fiorderBo.GetTenure(Convert.ToInt32(ddlSeries.SelectedValue), out minTenure, out maxTenure);
            if (hdnMintenure.Value != hdnMaxtenure.Value)
            {
                // txtMaturDate.Enabled = false;
                DateTime dt = txtOrderDate.SelectedDate.Value;
                dt = dt.AddMonths(Convert.ToInt32(hdnMaxtenure.Value));
                if (txtMaturDate.SelectedDate.Value > dt.Date)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('You cant select more than.' );" + dt.Date, true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FIxedIncomeOrderEntry", "alert('You cant select more than'  + dt.Date);", true);
                    // txtMaturDate.SelectedDate = "01 / 01 / 0001";
                }

            }
            DifferenceBetDates();

        }
        private void DifferenceBetDates()
        {
            if (txtMaturDate.SelectedDate.Value != null & txtOrderDate.SelectedDate.Value != null)
            {
                DateTime d2 = txtMaturDate.SelectedDate.Value;
                DateTime d1 = txtOrderDate.SelectedDate.Value;
                int x = 12 * (d2.Year - d1.Year) + (d1.Month - d2.Month);
                hdnMaxtenure.Value = x.ToString();
            }

        }


        //public void AddcLick(string[] args)
        //{
        //    bool blResult = false;
        //    bool blZeroBalance = false;
        //    bool blFileSizeExceeded = false;
        //    hdnOrderId.Value = args.ToString() ;

        //    if (fStorageBalance > 0)
        //        blResult = UploadFile(out blZeroBalance, out blFileSizeExceeded);
        //    else
        //        blZeroBalance = false;



        //    if (blZeroBalance)
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FIxedIncomeOrderEntry", "alert('You do not have enough space. You have only " + fStorageBalance + " MB left in your account!');", true);
        //    else
        //    {
        //        if (blResult)
        //        {

        //            // ResetControls();
        //            //  if (string.IsNullOrEmpty(linkAction))
        //            // {

        //            // }
        //            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Document uploaded Successfully!');", true);
        //            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseThePopUp", " CloseWindowsPopUp();", true);
        //        }
        //        else
        //        {
        //            //if (blFileSizeExceeded)
        //            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Sorry your Document file size exceeds the allowable 2 MB limit!');", true);
        //            //else
        //            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Error in uploading Document!');", true);
        //        }
        //    }

        //}
        protected void btnUploadImg_Click(object sender, EventArgs e)
        {
            // bool blResult = false;
            // bool blZeroBalance = false;
            // bool blFileSizeExceeded = false;
            // fStorageBalance = repoBo.GetAdviserStorageValues(advisorVo.advisorId, out fMaxStorage);
            //if (fStorageBalance > 0)
            //     blResult = UploadFile(out blZeroBalance, out blFileSizeExceeded);
            //else
            //    blZeroBalance = false ;



            // if (blZeroBalance)
            //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FIxedIncomeOrderEntry", "alert('You do not have enough space. You have only " + fStorageBalance + " MB left in your account!');", true);
            // else
            // {
            //     if (blResult)
            //     {

            //         // ResetControls();
            //         //  if (string.IsNullOrEmpty(linkAction))
            //         // {

            //         // }
            //    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Document uploaded Successfully!');", true);
            //         // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseThePopUp", " CloseWindowsPopUp();", true);
            //     }
            //     else
            //     {
            //         //if (blFileSizeExceeded)
            //         //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Sorry your Document file size exceeds the allowable 2 MB limit!');", true);
            //         //else
            //         //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Error in uploading Document!');", true);
            //     }
            // }


        }
        public void LoadNominees()
        {
            customerVo.CustomerId = customerid;
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

                dtCustomerAssociates.Columns.Add("MemberCustomerId");
                dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociates.Columns.Add("Name");
                dtCustomerAssociates.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {

                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates.Rows.Count > 0)
                {
                    gvNominees.DataSource = dtCustomerAssociates;
                    gvNominees.DataBind();
                    gvNominees.Visible = true;

                    trNoNominee.Visible = false;
                    //trNomineeCaption.Visible = true;
                    trNominees.Visible = true;
                }
                else
                {
                    trNoNominee.Visible = true;
                    //trNomineeCaption.Visible = false;
                    trNominees.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void TaxStatus()
        {


        }
        private double SimpleInterest(double principal, double interestRate, double years)
        {
            //A = P(1 + rt)

            //    double body = 1 + (interestRate / timesPerYear);
            double i = Convert.ToDouble(years / 12);
            double a = principal * (1 + interestRate * i); //* System.Math.Pow(body, exponent);
            return a;

        }

        private Int64 GetFaceValue()
        {
            Int64 faceValue = 0;
            if (!string.IsNullOrEmpty(ddlScheme.SelectedValue))
            {

                faceValue = fiorderBo.GetFaceValue(Convert.ToInt32(ddlScheme.SelectedValue));
            }
            return faceValue;
        }

        protected void OnQtytchanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                TxtPurAmt.Text = (Convert.ToInt32(txtQty.Text) * GetFaceValue()).ToString();
            }
        }

        protected void OnPayAmtTextchanged(object sender, EventArgs e)
        {
            double Maturityvalue;
            if (hdnMaxtenure.Value == "")
                return;
            int SchemePeriod = Convert.ToInt32(hdnMaxtenure.Value);
            if (ddlSchemeOption.SelectedValue == "NonCummulative")
            {


                if (hdnFrequency.Value == "0" | hdnFrequency.Value == "")
                {
                    txtPayAmt.Text = "";
                    txtMatAmt.Text = "";
                    Label11.Visible = false;
                    return;
                }
                else
                {
                    Label11.Visible = true;
                }

                if (!string.IsNullOrEmpty(txtPayAmt.Text) & txtPayAmt.Visible == true & Label18.Visible == true)
                {
                    Maturityvalue = CompoundInterest(Convert.ToDouble(txtPayAmt.Text), Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100, Convert.ToInt32(hdnFrequency.Value), SchemePeriod);
                    Maturityvalue = Math.Round(Maturityvalue, 3);
                    //Convert.ToDouble(Math.Round (SchemePeriod / 12,5))
                    txtMatAmt.Text = Maturityvalue.ToString(); //
                    Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtPayAmt.Text) - Maturityvalue).ToString();
                }
                else if (!string.IsNullOrEmpty(txtRenAmt.Text) & txtPayAmt.Visible == false & Label18.Visible == false)
                {
                    Maturityvalue = CompoundInterest(Convert.ToDouble(txtRenAmt.Text), Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100, Convert.ToInt32(hdnFrequency.Value), SchemePeriod);
                    Maturityvalue = Math.Round(Maturityvalue, 3);

                    txtMatAmt.Text = Maturityvalue.ToString();
                    Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtRenAmt.Text) - Maturityvalue).ToString();

                    // Convert.ToString(Convert.ToDouble(txtRenAmt.Text) + (Convert.ToDouble(txtRenAmt.Text) * Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100));
                }
            }
            if (ddlSchemeOption.SelectedValue == "Cummulative")
            {
                double i = Convert.ToDouble(SchemePeriod / 12);
                if (!string.IsNullOrEmpty(txtPayAmt.Text) & txtPayAmt.Visible == true & Label18.Visible == true)
                {
                    //  Maturityvalue = Convert.ToString(Convert.ToDouble(txtPayAmt.Text) + (Convert.ToDouble(txtPayAmt.Text) * (Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100) * (i)));
                    Maturityvalue = SimpleInterest(Convert.ToDouble(txtPayAmt.Text), (Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100), SchemePeriod);

                    Maturityvalue = Math.Round(Maturityvalue, 3);
                    txtMatAmt.Text = Maturityvalue.ToString();
                    Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtPayAmt.Text) - Maturityvalue).ToString();

                }
                else if (!string.IsNullOrEmpty(txtRenAmt.Text) & txtPayAmt.Visible == false & Label18.Visible == false)
                {
                    Maturityvalue = SimpleInterest(Convert.ToDouble(txtRenAmt.Text), (Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100), SchemePeriod);

                    Maturityvalue = Math.Round(Maturityvalue, 3);
                    txtMatAmt.Text = Maturityvalue.ToString();
                    Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtRenAmt.Text) - Maturityvalue).ToString();

                    // txtRenAmt.Text=  Convert.ToString(Convert.ToDouble(txtRenAmt.Text) + (Convert.ToDouble(txtRenAmt.Text) * Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100));

                }
            }


        }
        private int SchemePlan()
        {
            string a = ddlSeries.SelectedItem.Text;


            //String[] SchemePeriod = s.Split('-');


            //string a = SchemePeriod[1];
            string b = string.Empty;
            int val = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }
            if (b.Length > 0)
                val = int.Parse(b);




            // int a = Convert.ToInt32(SchemePeriod[1]);

            //  int b = Convert.ToInt32(SchemePeriod[1]);

            //int c=  SchemePeriod[1].Substring(0, 2);


            //  int c;
            //  if (b != 0)
            //      // for 18-24
            //      c = a - b;
            //  else
            //      c = a;

            return val;


        }
        private double CompoundInterest(double principal, double interestRate, int timesPerYear, double years)
        {
            //timesPerYear = 2; // Half yearly 2 // Monthly 12 //yearly 
            // (1 + r/n)
            double body = 1 + (interestRate / timesPerYear);
            // body = Math.Round(body, 4);
            double i = Convert.ToDouble(years / 12);

            // nt
            double exponent = timesPerYear * i;
            double a = principal * System.Math.Pow(body, exponent);
            // P(1 + r/n)^nt
            return a;
        }
        //private bool UploadFile(out bool blZeroBalance, out bool blFileSizeExceeded)
        //{
        //    // We need to see if the adviser has a folder in Vault imgPath retrieved from the web.config
        //    // Case 1: If not, then encode the adviser id and create a folder with the encoded id
        //    // then create a folder for the repository category within the encoded folder
        //    // then store the encoded advisor_adviserID + customerID + GUID + file name
        //    // Case 2: If folder exists, check if the category folder exists. 
        //    // If not then, create a folder with the category code and store the file as done above.
        //    // If yes, then just store the file as done above.
        //    // Once this is done, store the info in the DB with the file imgPath.

        //    string Temppath = System.Configuration.ConfigurationManager.AppSettings["UploadCustomerProofImages"];
        //    string UploadedFileName = String.Empty;
        //    bool blResult = false;
        //    blZeroBalance = false;
        //    blFileSizeExceeded = false;
        //    string extension = String.Empty;
        //    float fileSize = 0;

        //    try
        //    {
        //        customerVo.CustomerId = customerid;
        //        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

        //        // Uploading of file mandatory during button submit
        //        if (radUploadProof.UploadedFiles.Count != 0)
        //        {
        //            // Put this part under a transaction scope
        //            using (TransactionScope scope1 = new TransactionScope())
        //            {
        //                UploadedFile file = radUploadProof.UploadedFiles[0];
        //                fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB
        //                extension = file.GetExtension();

        //                // If space is there to upload file
        //                //saiif (fStorageBalance >= fileSize)
        //                //sai{
        //                    if (fileSize <= 2)   // If upload file size is less than 2 MB then upload
        //                    {
        //                        // Check if directory for advisor exists, and if not then create a new directoty
        //                        if (Directory.Exists(Temppath))
        //                        {
        //                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        //                            if (!System.IO.Directory.Exists(imgPath))
        //                            {
        //                                System.IO.Directory.CreateDirectory(imgPath);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            System.IO.Directory.CreateDirectory(Temppath);
        //                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        //                            if (!System.IO.Directory.Exists(imgPath))
        //                            {
        //                                System.IO.Directory.CreateDirectory(imgPath);
        //                            }
        //                            //imgPath = Server.MapPath("TempCustomerProof") + "\\advisor_" + rmVo.AdviserId + "\\";
        //                        }

        //                        strGuid = Guid.NewGuid().ToString();
        //                        string newFileName = SaveFileIntoServer(file, strGuid, imgPath, customerVo.CustomerId);

        //                        // Update DB with details
        //                        CPUVo.CustomerId = customerVo.CustomerId;
        //                        CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
        //                        CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
        //                        //CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
        //                        CPUVo.ProofImage = imgPath + "\\" + newFileName;

        //                            blResult = CreateDBReferrenceForProofUploads(CPUVo);

        //                        if (blResult)
        //                        {
        //                            // Once the adding of Document is a success, then update the balance storage in advisor subscription table
        //                         //   fStorageBalance = UpdateAdvisorStorageBalance(fileSize, 0, fStorageBalance);
        //                            LoadImages();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        blFileSizeExceeded = true;
        //                    }
        //                //}
        //                //else
        //                //{
        //                //    blZeroBalance = true;
        //                //}

        //                scope1.Complete();   // Commit the transaction scope if no errors
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a document file to upload!');", true);
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        //object[] objects = new object[1];
        //        //objects[0] = CPUVo;
        //        //PageException(objects, Ex, "ViewCustomerProofs.ascx:AddClick()");
        //    }
        //    return blResult;

        //    #region Old Code

        //    //    FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.AllAccess, imgPath);
        //    //    PermissionSet ps = new PermissionSet(PermissionState.None);
        //    //    ps.AddPermission(fp);
        //    //    DirectoryInfo[] DI = new DirectoryInfo(imgPath).GetDirectories("*.*", SearchOption.AllDirectories);
        //    //    FileInfo[] FI = new DirectoryInfo(imgPath).GetFiles("*.*", SearchOption.AllDirectories);

        //    //    foreach (FileInfo F1 in FI)
        //    //    {
        //    //        DirSize += F1.Length;
        //    //    }
        //    //    //Converting in Mega bytes
        //    //    DirSize = DirSize / 1048576;

        //    //    #region Update Code 1

        //    //    if (Session["ImagePath"] != null)
        //    //    {
        //    //        // If Uploaded File Exists
        //    //        FileInfo fi = new FileInfo(Session["ImagePath"].ToString());
        //    //        float alreadyUploadedFileSize = fi.Length;
        //    //        alreadyUploadedFileSize = alreadyUploadedFileSize / 1048576;
        //    //        DirSize = DirSize - alreadyUploadedFileSize;
        //    //    }

        //    //    #endregion

        //    //    if ((fileSize < adviserVo.VaultSize) && (DirSize < adviserVo.VaultSize))
        //    //    {
        //    //        foreach (UploadedFile f in radUploadProof.UploadedFiles)
        //    //        {
        //    //            int l = (int)f.InputStream.Length;
        //    //            byte[] bytes = new byte[l];
        //    //            f.InputStream.Read(bytes, 0, l);

        //    //            imageUploadPath = customerVo.CustomerId + "_" + guid + "_" + f.GetName();
        //    //            if (btnSubmit.Text == "Submit")
        //    //            {
        //    //                // Submit part
        //    //                if (extension != ".pdf")
        //    //                {
        //    //                    UploadImage(imgPath, f, imageUploadPath);
        //    //                }
        //    //                else
        //    //                {
        //    //                    f.SaveAs(imgPath + "\\" + imageUploadPath);
        //    //                }
        //    //            }

        //    //            #region Update Code 2

        //    //            else
        //    //            {
        //    //                if (extension != ".pdf")
        //    //                {
        //    //                    File.Delete(imgPath + UploadedFileName);
        //    //                    DataTable dtGetPerticularProofs = new DataTable();
        //    //                    if (Session["ProofID"] != null)
        //    //                    {
        //    //                        int ProofIdToDelete = Convert.ToInt32(Session["ProofID"].ToString());
        //    //                        dtGetPerticularProofs = GetUploadedImagePaths(ProofIdToDelete);
        //    //                        string imageAttachmentPath = dtGetPerticularProofs.Rows[0]["CPU_Image"].ToString();
        //    //                        if (customerBo.DeleteCustomerUploadedProofs(customerVo.CustomerId, ProofIdToDelete))
        //    //                        {
        //    //                            File.Delete(imageAttachmentPath);
        //    //                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "loadcontrol('ViewCustomerProofs','login');", true);
        //    //                        }
        //    //                    }
        //    //                }
        //    //                f.SaveAs(imgPath + "\\" + imageUploadPath);
        //    //            }

        //    //            #endregion

        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry your Document attachment size exceeds the allowable limit..!');", true);
        //    //    }
        //    //}

        //    //CPUVo.CustomerId = customerVo.CustomerId;
        //    //CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
        //    //CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
        //    //CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
        //    //if (imageUploadPath == "")
        //    //    CPUVo.ProofImage = imgPath + "\\" + UploadedFileName;
        //    //else
        //    //    CPUVo.ProofImage = imgPath + "\\" + imageUploadPath;
        //    //CreateDBReferrenceForProofUploads(CPUVo);

        //    //LoadImages();
        //    //Session["ImagePath"] = null;

        //    #endregion
        //}
        //private bool CreateDBReferrenceForProofUploads(CustomerProofUploadsVO CPUVo)
        //{
        //    string createOrUpdate = "";
        //    int proofUploadID = 0;
        //    bool bStatus = false;

        //    //if (btnSubmit.Text.Trim().Equals("Submit"))
        //    //{
        //        createOrUpdate = "Submit";
        //        if (CPUVo != null &   hdnOrderId.Value != "0")
        //        {
        //             customerBo.CreateCustomerOrderDocument(CPUVo, Convert.ToInt32(hdnOrderId.Value));
        //        }
        //    //}
        //    //else if (btnSubmit.Text.Trim().Equals(Constants.Update.ToString()))
        //    //{
        //    //    createOrUpdate = Constants.Update.ToString();
        //    //    proofUploadID = Convert.ToInt32(Session["ProofID"].ToString());
        //    //    if (CPUVo != null)
        //    //    {
        //    //        bStatus = customerBo.CreateCustomersProofUploads(CPUVo, proofUploadID, createOrUpdate);
        //    //    }
        //    //}

        //    return bStatus;
        //}

        //private void UploadImage(string imgPath, UploadedFile f, string imageUploadPath)
        //{
        //    TargetPath = imgPath;

        //    UploadedFile jpeg_image_upload = f;

        //    // Retrieve the uploaded image
        //    original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
        //    original_image.Save(TargetPath + imageUploadPath);//"O_" + 

        //    int width = original_image.Width;
        //    int height = original_image.Height;
        //    //int new_width, new_height;

        //    //int target_width = 140;
        //    //int target_height = 100;

        //    //CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, imageUploadPath); // , out thumbnail_id

        //    //File.Delete(TargetPath + "O_" + System.IO.imgPath.GetFileName(jpeg_image_upload.FileName));
        //}
        //private string SaveFileIntoServer(UploadedFile file, string strGuid, string strPath, int intCustId)
        //{
        //    string fileExtension = String.Empty;
        //    fileExtension = file.GetExtension();
        //    string strRenameFilename = file.GetName();
        //    strRenameFilename = strRenameFilename.Replace(' ', '_');
        //    string newFileName = intCustId + "_" + strGuid + "_" + strRenameFilename;

        //    // Save Document file in the imgPath
        //    if (fileExtension != ".pdf")
        //        UploadImage(strPath, file, newFileName);
        //    else
        //        file.SaveAs(strPath + "\\" + newFileName);

        //    return newFileName;
        //}
        //private void saveJpeg(string p, System.Drawing.Bitmap final_image, int p_3)
        //{
        //    // Encoder parameter for image quality
        //    EncoderParameter qualityParam =
        //       new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)p_3);

        //    // Jpeg image codec
        //    ImageCodecInfo jpegCodec =
        //       this.getEncoderInfo("image/png");

        //    if (jpegCodec == null)
        //        return;

        //    EncoderParameters encoderParams = new EncoderParameters(1);
        //    encoderParams.Param[0] = qualityParam;

        //    final_image.Save(p, jpegCodec, encoderParams);
        //}
        private DataTable GetUploadedImagePaths(int ProofUploadId)
        {
            customerVo.CustomerId = customerid;
            DataTable dtImages = new DataTable();
            try
            {
                dtImages = customerBo.GetCustomerUploadedProofs(customerVo.CustomerId, ProofUploadId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetUploadedImagePaths()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtImages;
        }
        private void LoadImages()
        {
            //string Temppath = "";
            DataTable dtImages = new DataTable();
            DataTable dtBindImages = new DataTable();
            DataTable dtProofPurposes = new DataTable();
            dtBindImages.Columns.Add("ProofUploadId");
            dtBindImages.Columns.Add("ProofType");
            dtBindImages.Columns.Add("ProofName");
            dtBindImages.Columns.Add("ProofCopyType");
            dtBindImages.Columns.Add("ProofImage");
            dtBindImages.Columns.Add("ProofExtensions");
            dtBindImages.Columns.Add("ProofFileName");

            DataRow drBindImages = null;
            dtImages = GetUploadedImagePaths(0);
            System.Web.UI.WebControls.Image imageProof = new System.Web.UI.WebControls.Image();
            System.Web.UI.WebControls.HyperLink hypPdf = new HyperLink();
            string fileExt = "";

            int i = 0;
            //int fileCount = Directory.GetFiles(imgPath, "*.*", SearchOption.AllDirectories).Length;
            string sourceDir = "";

            //if (dtImages.Rows.Count > 0)
            //{
            //    foreach (DataRow drUploadImage in dtImages.Rows)
            //    {
            //        drBindImages = dtBindImages.NewRow();
            //        drBindImages["ProofUploadId"] = drUploadImage["CPU_ProofUploadId"];
            //        drBindImages["ProofType"] = drUploadImage["XPRT_ProofType"];
            //        drBindImages["ProofName"] = drUploadImage["XP_ProofName"];
            //        drBindImages["ProofCopyType"] = drUploadImage["XPCT_ProofCopyType"];
            //        drBindImages["ProofImage"] = drUploadImage["CPU_Image"];
            //        drBindImages["ProofExtensions"] = imgPath.GetExtension(drUploadImage["CPU_Image"].ToString());
            //        drBindImages["ProofFileName"] = imgPath.GetFileName(drUploadImage["CPU_Image"].ToString());

            //        dtBindImages.Rows.Add(drBindImages);
            //    }

            //    #region ??? Code

            //    if (dtBindImages.Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dtBindImages.Rows)
            //        {
            //            string fileTempPath = dr["ProofImage"].ToString();

            //            string extension = imgPath.GetExtension(fileTempPath);
            //            string fileName = imgPath.GetFileName(fileTempPath);
            //            Session["FileExtension"] = extension;
            //        }
            //    }


            //  #endregion

            //    repProofImages.DataSource = dtBindImages;
            //    repProofImages.DataBind();

            //    if (Session["Button"] != "")
            //    {
            //        if (Session["Button"] == "Submit")
            //        {
            //            if (Session["LinkAction"] != null)
            //            {
            //                radPOCProof.SelectedIndex = 0;
            //                multiPageView.SelectedIndex = 0;
            //                //this.Close();
            //            }
            //            else
            //            {
            //                radPOCProof.SelectedIndex = 1;
            //                multiPageView.SelectedIndex = 1;
            //            }
            //        }
            //        else if (Session["Button"] == "Submit & Add More")
            //        {
            //            radPOCProof.TabIndex = 0;
            //            multiPageView.TabIndex = 0;

            //            BindProofTypeDP();
            //            ddlProofType.SelectedIndex = 0;
            //            BindProofCopy();
            //            ddlProofCopyType.SelectedIndex = 0;
            //            ddlProof.SelectedIndex = 0;
            //            btnDelete.Visible = false;
            //        }
            //    }
            //}
            //else
            //{
            //    repProofImages.DataSource = null;
            //    ChangeTelerikRadTab(0);
            //}
        }
        //private void UploadImage(string imgPath, UploadedFile f, string imageUploadPath)
        //{
        //    TargetPath = imgPath;

        //    UploadedFile jpeg_image_upload = f;

        //    // Retrieve the uploaded image
        //    original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
        //    original_image.Save(TargetPath + imageUploadPath);//"O_" + 

        //    int width = original_image.Width;
        //    int height = original_image.Height;
        //    //int new_width, new_height;

        //    //int target_width = 140;
        //    //int target_height = 100;

        //    //CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, imageUploadPath); // , out thumbnail_id

        //    //File.Delete(TargetPath + "O_" + System.IO.imgPath.GetFileName(jpeg_image_upload.FileName));
        //}
        //private bool UploadFile(out bool blZeroBalance, out bool blFileSizeExceeded)
        //{
        //    // We need to see if the adviser has a folder in Vault imgPath retrieved from the web.config
        //    // Case 1: If not, then encode the adviser id and create a folder with the encoded id
        //    // then create a folder for the repository category within the encoded folder
        //    // then store the encoded advisor_adviserID + customerID + GUID + file name
        //    // Case 2: If folder exists, check if the category folder exists. 
        //    // If not then, create a folder with the category code and store the file as done above.
        //    // If yes, then just store the file as done above.
        //    // Once this is done, store the info in the DB with the file imgPath.

        //    string Temppath = System.Configuration.ConfigurationManager.AppSettings["UploadCustomerProofImages"];
        //    string UploadedFileName = String.Empty;
        //    bool blResult = false;
        //    blZeroBalance = false;
        //    blFileSizeExceeded = false;
        //    string extension = String.Empty;
        //    float fileSize = 0;

        //    try
        //    {
        //        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

        //        // Uploading of file mandatory during button submit
        //        if (radUploadProof.UploadedFiles.Count != 0)
        //        {
        //            // Put this part under a transaction scope
        //            using (TransactionScope scope1 = new TransactionScope())
        //            {
        //                UploadedFile file = radUploadProof.UploadedFiles[0];
        //                fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB
        //                extension = file.GetExtension();

        //                // If space is there to upload file
        //                if (fStorageBalance >= fileSize)
        //                {
        //                    if (fileSize <= 2)   // If upload file size is less than 2 MB then upload
        //                    {
        //                        // Check if directory for advisor exists, and if not then create a new directoty
        //                        if (Directory.Exists(Temppath))
        //                        {
        //                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        //                            if (!System.IO.Directory.Exists(imgPath))
        //                            {
        //                                System.IO.Directory.CreateDirectory(imgPath);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            System.IO.Directory.CreateDirectory(Temppath);
        //                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        //                            if (!System.IO.Directory.Exists(imgPath))
        //                            {
        //                                System.IO.Directory.CreateDirectory(imgPath);
        //                            }
        //                            //imgPath = Server.MapPath("TempCustomerProof") + "\\advisor_" + rmVo.AdviserId + "\\";
        //                        }

        //                        strGuid = Guid.NewGuid().ToString();
        //                        string newFileName = SaveFileIntoServer(file, strGuid, imgPath, customerVo.CustomerId);

        //                        // Update DB with details
        //                        CPUVo.CustomerId = customerVo.CustomerId;
        //                        CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
        //                        CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
        //                       // CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
        //                        CPUVo.ProofImage = imgPath + "\\" + newFileName;

        //                        blResult = CreateDBReferrenceForProofUploads(CPUVo);

        //                        if (blResult)
        //                        {
        //                            // Once the adding of Document is a success, then update the balance storage in advisor subscription table
        //                          //sai  fStorageBalance = UpdateAdvisorStorageBalance(fileSize, 0, fStorageBalance);
        //                            LoadImages();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        blFileSizeExceeded = true;
        //                    }
        //                }
        //                else
        //                {
        //                    blZeroBalance = true;
        //                }

        //                scope1.Complete();   // Commit the transaction scope if no errors
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a document file to upload!');", true);
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        object[] objects = new object[1];
        //        //objects[0] = CPUVo;
        //        //PageException(objects, Ex, "ViewCustomerProofs.ascx:AddClick()");
        //    }
        //    return blResult;


        //}

        private void GetCustomerAssociates(int customerid)
        {
            //DataSet dsAssociates = fiorderBo.GetCustomerAssociates(customerid);
            //gvAssociation.DataSource = dsAssociates.Tables[0];
            //gvAssociation.DataBind();
        }
        //protected void rgvOrderSteps_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    //DataSet dsAssociates = fiorderBo.GetCustomerAssociates(customerid);
        //    //this.rgvCustGrid.DataSource = dsAssociates.Tables[0];
        ////}
        private void FICategory()
        {
            DataSet dsBankName = fiorderBo.GetFICategory();




            if (dsBankName.Tables[0].Rows.Count > 0)
            {

                ddlCategory.DataSource = dsBankName;
                ddlCategory.DataValueField = dsBankName.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlCategory.DataTextField = dsBankName.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlCategory.DataBind();

                //  ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));

            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }


        private void GetFIModeOfHolding()
        {
            DataSet dsDepoBank = fiorderBo.GetFIModeOfHolding();


            if (dsDepoBank.Tables[0].Rows.Count > 0)
            {

                ddlModeofHOldingFI.DataSource = dsDepoBank.Tables[0];
                ddlModeofHOldingFI.DataValueField = dsDepoBank.Tables[0].Columns["XMOH_ModeOfHoldingCode"].ToString();
                ddlModeofHOldingFI.DataTextField = dsDepoBank.Tables[0].Columns["XMOH_ModeOfHolding"].ToString();
                ddlModeofHOldingFI.DataBind();

                ddlModeofHOldingFI.Items.Insert(0, new ListItem("Select", "Select"));

            }
            else
            {
                ddlModeofHOldingFI.Items.Clear();
                ddlModeofHOldingFI.DataSource = null;
                ddlModeofHOldingFI.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        private void FIIssuer(int AdviserId)
        {
            DataSet dsIssuer = fiorderBo.GetFIIssuer(AdviserId, ddlCategory.SelectedValue);
            if (dsIssuer.Tables[0].Rows.Count > 0)
            {
                ddlIssuer.DataSource = dsIssuer;
                ddlIssuer.DataValueField = dsIssuer.Tables[0].Columns["PFIIM_IssuerId"].ToString();
                ddlIssuer.DataTextField = dsIssuer.Tables[0].Columns["PFIIM_IssuerName"].ToString();
                ddlIssuer.DataBind();
                ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlIssuer.Items.Clear();
                ddlIssuer.DataSource = null;
                ddlIssuer.DataBind();
                ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            customerVo.CustomerId = customerid;

            try
            {
                if (rbtnYes.Checked)
                {
                    ddlModeofHOldingFI.Enabled = true;

                    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

                    dtCustomerAssociates.Columns.Clear();
                    dtCustomerAssociates.Columns.Add("MemberCustomerId");
                    dtCustomerAssociates.Columns.Add("AssociationId");
                    dtCustomerAssociates.Columns.Add("Name");
                    dtCustomerAssociates.Columns.Add("Relationship");

                    foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                    {
                        drCustomerAssociates = dtCustomerAssociates.NewRow();
                        drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                        drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                        drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                        drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                        dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                    }

                    if (dtCustomerAssociates.Rows.Count > 0)
                    {
                        trNoJointHolders.Visible = false;
                        trJoinHolders.Visible = true;
                        trJointHolderGrid.Visible = true;
                        gvJointHoldersList.DataSource = dtCustomerAssociates;
                        gvJointHoldersList.DataBind();
                        gvJointHoldersList.Visible = true;
                    }
                    else
                    {
                        trNoJointHolders.Visible = true;
                        trJoinHolders.Visible = false;
                        trJointHolderGrid.Visible = false;
                    }
                    ddlModeofHOldingFI.SelectedIndex = 0;
                }
                else
                {
                    ddlModeofHOldingFI.SelectedValue = "SI";
                    ddlModeofHOldingFI.Enabled = false;
                    trJoinHolders.Visible = false;
                    trJointHolderGrid.Visible = false;
                    trNoJointHolders.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:rbtnYes_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void FIScheme(int AdviserId, string IssuerID)
        {
            DataSet dsScheme = fiorderBo.GetFIScheme(AdviserId, IssuerID,0);
            if (dsScheme.Tables[0].Rows.Count > 0)
            {
                ddlScheme.DataSource = dsScheme;
                ddlScheme.DataValueField = dsScheme.Tables[0].Columns["PFISM_SchemeId"].ToString();
                ddlScheme.DataTextField = dsScheme.Tables[0].Columns["PFISM_SchemeName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.DataSource = null;
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        //FISeriesDetails
        private void FISeriesDetails(int SeriesID)
        {
            DataSet dsScheme = fiorderBo.GetFISeriesDetailssDetails(SeriesID);
            DataTable dtSeriesDetails = dsScheme.Tables[0];
            string Tenure;
            string CouponType;

            if (dtSeriesDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSeriesDetails.Rows)
                {
                    Tenure = dr["PFISD_Tenure"].ToString();
                    hdnDefaulteInteresRate.Value = dr["PFISD_defaultInterestRate"].ToString();
                    CouponType = dr["PFISD_CouponType"].ToString();
                    txtSeries.Text = "Tenure-" + Tenure + "/" + "InterestRate-" + hdnDefaulteInteresRate.Value + "/" + "InterestType-" + CouponType;
                    Label10.Text = txtSeries.Text;
                }



            }


        }
        private void FISeries(int SeriesID)
        {
            DataSet dsScheme = fiorderBo.GetFISeries(SeriesID);
            if (dsScheme.Tables[0].Rows.Count > 0)
            {
                ddlSeries.DataSource = dsScheme;
                ddlSeries.DataValueField = dsScheme.Tables[0].Columns["PFISD_SeriesId"].ToString();
                ddlSeries.DataTextField = dsScheme.Tables[0].Columns["PFISD_SeriesName"].ToString();
                ddlSeries.DataBind();
                //ddlSeries.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlSeries.Items.Clear();
                ddlSeries.DataSource = null;
                ddlSeries.DataBind();
                ddlSeries.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        private void BindBank(int customerId)
        {
            //DataSet dsBankName = mfOrderBo.GetCustomerBank(customerId);
            //if (dsBankName.Tables[0].Rows.Count > 0)
            //{
            //    ddlBankName.DataSource = dsBankName;
            //    ddlBankName.DataValueField = dsBankName.Tables[0].Columns["CB_CustBankAccId"].ToString();
            //    ddlBankName.DataTextField = dsBankName.Tables[0].Columns["WERPBM_BankName"].ToString();
            //    ddlBankName.DataBind();
            //    ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            //}
            //else
            //{
            //    ddlBankName.Items.Clear();
            //    ddlBankName.DataSource = null;
            //    ddlBankName.DataBind();
            //    ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            //}
        }

        protected void ddlSchemeOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFrequency.SelectedIndex = 3;

            if (ddlSchemeOption.SelectedValue == "NonCummulative")
            {
                ddlFrequency.Enabled = true;

            }
            else
            {
                ddlFrequency.Enabled = false;

            }
            OnPayAmtTextchanged(this, null);
        }

        protected void ddlFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Half yearly 2 // Monthly 12 //yearly 
            int Val = 0;
            if (ddlFrequency.SelectedValue == "Monthly")
            {
                Val = 12;

            }
            else if (ddlFrequency.SelectedValue == "Quarterly")
            {
                Val = 4;
            }
            else if (ddlFrequency.SelectedValue == "Yearly")
            {
                Val = 1;
            }
            else if (ddlFrequency.SelectedValue == "Hfyearly")
            {
                Val = 2;

            }
            hdnFrequency.Value = Val.ToString();
            OnPayAmtTextchanged(this, null);
        }

        protected void ddlTranstype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTranstype.SelectedValue == "Select")
            {
                trDepRen.Visible = false;
                //trPayAmt.Visible = false;
                txtPayAmt.Visible = false;
                Label18.Visible = false;

            }
            else if (ddlTranstype.SelectedValue == "Renewal")
            {
                trDepRen.Visible = true;
                ////trPayAmt.Visible = false;
                txtPayAmt.Visible = false;
                Label18.Visible = false;
            }
            else if (ddlTranstype.SelectedValue == "New")
            {
                txtPayAmt.Visible = true;
                Label18.Visible = true;
                //trPayAmt.Visible = true;
                trDepRen.Visible = false;
            }

        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex != 0)
                FIIssuer(advisorVo.advisorId);
            if (ddlCategory.SelectedValue == "FICG")
            {
                trSchemeOpFreq.Visible = false;
                trDepPaypriv.Visible = false;
                Label8.Text = "Capital Amount";
            }
            else
            {
                trSchemeOpFreq.Visible = true;
                trDepPaypriv.Visible = true;
                Label8.Text = "FD Amount";
            }
        }

        protected void SetEnabilityControls()
        {
            if (ddlCategory.SelectedIndex != 0)
                FIIssuer(advisorVo.advisorId);
            if (ddlCategory.SelectedValue == "FICG")
            {
                trSchemeOpFreq.Visible = false;
                trDepPaypriv.Visible = false;
                Label8.Text = "Capital Amount";
            }
            else
            {
                trSchemeOpFreq.Visible = true;
                trDepPaypriv.Visible = true;
                Label8.Text = "FD Amount";
            }
        }

        protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
                FIScheme(advisorVo.advisorId, ddlIssuer.SelectedValue);
        }
        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedValue != "Select")
                FISeries(Convert.ToInt32(ddlScheme.SelectedValue));


            int minTenure = 0;
            int maxTenure = 0;

            if (ddlSeries.SelectedValue != "0")
            {
                FISeriesDetails(Convert.ToInt32(ddlSeries.SelectedValue));


                if (ddlSeries.SelectedValue != "0")
                    fiorderBo.GetTenure(Convert.ToInt32(ddlSeries.SelectedValue), out minTenure, out maxTenure);

                hdnMintenure.Value = minTenure.ToString();
                hdnMaxtenure.Value = maxTenure.ToString();
            }
            OnPayAmtTextchanged(this, null);
        }

        protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            int minTenure = 0;
            int maxTenure = 0;

            if (ddlSeries.SelectedIndex > 0)
            {
                FISeriesDetails(Convert.ToInt32(ddlSeries.SelectedValue));


                if (ddlSeries.SelectedValue != "0")
                    fiorderBo.GetTenure(Convert.ToInt32(ddlSeries.SelectedValue), out minTenure, out maxTenure);

                hdnMintenure.Value = minTenure.ToString();
                hdnMaxtenure.Value = maxTenure.ToString();
            }
            OnPayAmtTextchanged(this, null);
        }
        protected void ddlProofType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlProofType.SelectedIndex != 0)
            //    BindddlProof(Convert.ToInt32(ddlProofType.SelectedValue));
        }

        protected void BindddlProof(int proofTypeSelectedValue)
        {
            //DataTable dtDpProofsForTypes = new DataTable();
            //dtDpProofsForTypes = customerBo.GetCustomerProofsForTypes(proofTypeSelectedValue);

            //ddlProof.Items.Clear();
            //ddlProof.SelectedValue = null;
            //if (dtDpProofsForTypes.Rows.Count > 0)
            //{
            //    ddlProof.DataSource = dtDpProofsForTypes;
            //    ddlProof.DataValueField = dtDpProofsForTypes.Columns["XP_ProofCode"].ToString();
            //    ddlProof.DataTextField = dtDpProofsForTypes.Columns["XP_ProofName"].ToString();
            //    ddlProof.DataBind();
            //}
            //ddlProof.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindProofTypeDP()
        {
            //DataTable dtDpProofTypes = new DataTable();
            //dtDpProofTypes = customerBo.GetCustomerProofTypes();

            //if (dtDpProofTypes.Rows.Count > 0)
            //{
            //    ddlProofType.DataSource = dtDpProofTypes;
            //    ddlProofType.DataValueField = dtDpProofTypes.Columns["XPRT_ProofTypeCode"].ToString();
            //    ddlProofType.DataTextField = dtDpProofTypes.Columns["XPRT_ProofType"].ToString();
            //    ddlProofType.DataBind();
            //}
            //ddlProofType.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtOrderDate_DateChanged(object sender, EventArgs e)
        {


            if (hdnMintenure.Value == hdnMaxtenure.Value)
            {
                txtMaturDate.Enabled = false;
                DateTime dt = txtOrderDate.SelectedDate.Value;
                dt = dt.AddMonths(Convert.ToInt32(hdnMaxtenure.Value));
                txtMaturDate.SelectedDate = dt.Date;
            }
            else
            {


                txtMaturDate.Enabled = true;
            }
        }

        protected void txtApplicationDt_DateChanged(object sender, EventArgs e)
        {
        }
        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                //trJointHoldersList.Visible = false;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                // ddlAMCList.Enabled = true;
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                //txtBranch.Text = customerVo.BranchName;
                //txtRM.Text = customerVo.RMName;
                //txtPanSearch.Text = customerVo.PANNum;
                //hdnCustomerId.Value = txtCustomerId.Value;
                //customerId = int.Parse(txtCustomerId.Value);
                // if (ddlsearch.SelectedItem.Value == "2")
                //     lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;

                //= rmVo.FirstName + ' ' + rmVo.MiddleName + ' ' + rmVo.LastName;
                // BindBank(customerId);
                //BindPortfolioDropdown(customerId);
                // ddltransType.SelectedIndex = 0;
                //  BindISAList();
            }
        }


        protected void lnkBtnDemat_onClick(object sender, EventArgs e)
        {
            customerid = Convert.ToInt32(Session["customerid"]);

            GetDematAccountDetails(customerid);
            rwDematDetails.VisibleOnPageLoad = true;

        }

        protected void btnAddDemat_Click(object sender, EventArgs e)
        {
            int dematAccountId = 0;
            foreach (GridDataItem gvr in gvDematDetailsTeleR.MasterTableView.Items)
            {
                if (((CheckBox)gvr.FindControl("chkDematId")).Checked == true)
                {
                    dematAccountId = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DematAccountId"].ToString());
                    txtDematid.Text = gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DPClientId"].ToString();
                    break;
                }

            }
            BindgvFamilyAssociate(dematAccountId);

        }
        private void BindgvFamilyAssociate(int demataccountid)
        {
            gvAssociate.Visible = true;
            DataSet dsAssociate = boDematAccount.GetCustomerDematAccountAssociates(demataccountid);
            gvAssociate.DataSource = dsAssociate;
            gvAssociate.DataBind();
            pnlJointHolderNominee.Visible = true;
            if (Cache["gvAssociate" + userVo.UserId] == null)
            {
                Cache.Insert("gvAssociate" + userVo.UserId, dsAssociate);
            }
            else
            {
                Cache.Remove("gvAssociate" + userVo.UserId);
                Cache.Insert("gvAssociate" + userVo.UserId, dsAssociate);
            }
        }
        private void GetDematAccountDetails(int customerId)
        {
            try
            {
                DataSet dsDematDetails = boDematAccount.GetDematAccountHolderDetails(customerId);
                gvDematDetailsTeleR.Visible = true;
                gvDematDetailsTeleR.DataSource = dsDematDetails.Tables[0];
                gvDematDetailsTeleR.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}