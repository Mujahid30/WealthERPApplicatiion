using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUploads;
using VoCustomerProfiling;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using BoCommon;
using System.Data;
using WealthERP.Base;
namespace WealthERP.Customer
{
    public partial class EditCustomerNonIndividualProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        string path = "";
        DataTable dtCustomerSubType = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            customerVo = (CustomerVo)Session["CustomerVo"];
            try
            {
                SessionBo.CheckSession();
                if (!IsPostBack)
                {
                    
                    BindDropDowns(path);
                    //Bind Adviser Branch List
                    BindListBranch(customerVo.RmId, "rm");

                    userVo = (UserVo)Session["userVo"];
                    if (customerVo.Type.ToUpper().ToString() == "IND")
                    {
                        
                        rbtnIndividual.Checked = true;
                    }
                    else
                    {
                        rbtnNonIndividual.Checked = true;
                    }

                    if (customerVo.ProfilingDate == DateTime.MinValue)
                    {
                        txtDateofProfiling.Text = DateTime.Today.ToShortDateString();
                    }
                    else
                        txtDateofProfiling.Text = customerVo.ProfilingDate.ToShortDateString();
                    ddlAdviserBranchList.SelectedValue = customerVo.BranchId.ToString();
                    txtFirstName.Text = customerVo.ContactFirstName;
                    txtMiddleName.Text = customerVo.ContactMiddleName;
                    txtLastName.Text = customerVo.ContactLastName;

                    txtCompanyWebsite.Text = customerVo.CompanyWebsite.ToString();
                    txtRegistrationPlace.Text = customerVo.RegistrationPlace.ToString();
                    txtRocRegistration.Text = customerVo.RegistrationNum.ToString();
                    if (customerVo.RegistrationDate == DateTime.MinValue)
                    {
                        txtDateofRegistration.Text = "";
                    }
                    else
                        txtDateofRegistration.Text = customerVo.RegistrationDate.ToShortDateString();

                    if (customerVo.CommencementDate == DateTime.MinValue)
                    {
                        txtDateofCommencement.Text = "";
                    }
                    else
                        txtDateofCommencement.Text = customerVo.CommencementDate.ToShortDateString();
                    txtCompanyName.Text = customerVo.FirstName;
                    txtCustomerCode.Text = customerVo.CustCode.ToString();
                    txtPanNumber.Text = customerVo.PANNum.ToString();
                    //txtRmName.Text = customerVo.RmId.ToString();
                    txtCorrAdrLine1.Text = customerVo.Adr1Line1.ToString();
                    txtCorrAdrLine2.Text = customerVo.Adr1Line2.ToString();
                    txtCorrAdrLine3.Text = customerVo.Adr1Line3.ToString();
                    txtCorrAdrPinCode.Text = customerVo.Adr1PinCode.ToString();
                    txtCorrAdrCity.Text = customerVo.Adr1City.ToString();
                    if (customerVo.Adr1State != "")
                    {
                        ddlCorrAdrState.SelectedValue = customerVo.Adr1State;
                    }
                    if (customerVo.DummyPAN == 1)
                    {
                        chkdummypan.Checked = true;
                    }
                    else
                    {
                        chkdummypan.Checked = false;
                    }
                    if (customerVo.IsProspect == 1)
                    {
                        chkprospectn.Checked = true;
                    }
                    else
                    {
                        chkprospectn.Checked = false;
                    }
                    if (customerVo.ViaSMS == 1)
                    {
                        chksmsn.Checked = true;
                    }
                    else
                    {
                        chksmsn.Checked = false;
                    }
                    if (customerVo.AlertViaEmail == 1)
                    {
                        chkmailn.Checked = true;
                    }
                    else
                    {
                        chkmailn.Checked = false;
                    }

                    ddlCorrAdrCountry.SelectedItem.Value = customerVo.Adr1Country.ToString();
                    txtPermAdrLine1.Text = customerVo.Adr2Line1.ToString();
                    txtPermAdrLine2.Text = customerVo.Adr2Line2.ToString();
                    txtPermAdrLine3.Text = customerVo.Adr2Line3.ToString();
                    txtPermAdrPinCode.Text = customerVo.Adr2PinCode.ToString();
                    txtPermAdrCity.Text = customerVo.Adr2City.ToString();
                    if (customerVo.Adr2State.ToString() != "")
                    {
                        ddlPermAdrState.SelectedValue = customerVo.Adr2State;
                    }
                    ddlPermAdrCountry.SelectedItem.Value = customerVo.Adr2Country.ToString();
                    txtPhoneNo1Isd.Text = customerVo.ResISDCode.ToString();
                    txtPhoneNo1Std.Text = customerVo.ResSTDCode.ToString();
                    txtPhoneNo1.Text = customerVo.ResPhoneNum.ToString();
                    txtPhoneNo2Isd.Text = customerVo.OfcISDCode.ToString();
                    txtPhoneNo2Std.Text = customerVo.OfcSTDCode.ToString();
                    txtPhoneNo2.Text = customerVo.OfcPhoneNum.ToString();
                    txtFax.Text = customerVo.Fax.ToString();
                    txtFaxIsd.Text = customerVo.ISDFax.ToString();
                    txtFaxStd.Text = customerVo.STDFax.ToString();
                    txtEmail.Text = customerVo.Email.ToString();
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
                FunctionInfo.Add("Method", "EditCustomerNonIndividualProfile.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void BindDropDowns(string path)
        {
            DataTable dtStates = XMLBo.GetStates(path);
            ddlCorrAdrState.DataSource = dtStates;
            ddlCorrAdrState.DataValueField = "StateCode";
            ddlCorrAdrState.DataTextField = "StateName";
            ddlCorrAdrState.DataBind();
            ddlCorrAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));


            ddlPermAdrState.DataSource = dtStates;
            ddlPermAdrState.DataValueField = "StateCode";
            ddlPermAdrState.DataTextField = "StateName";
            ddlPermAdrState.DataBind();
            ddlPermAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

            if (customerVo.Type.ToUpper().ToString() == "IND")
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");

            }
            else
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
            }
            ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataTextField = "CustomerTypeName";
            ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            ddlCustomerSubType.SelectedValue = customerVo.SubType;

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtnIndividual.Checked)
                {
                    customerVo.Type = "IND";
                }
                else
                    customerVo.Type = "NIND";
                customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue.ToString());
                customerVo.SubType = ddlCustomerSubType.SelectedItem.Value.ToString();
                customerVo.ContactFirstName = txtFirstName.Text.ToString();
                customerVo.ContactMiddleName = txtMiddleName.Text.ToString();
                customerVo.ContactLastName = txtLastName.Text.ToString();
                customerVo.FirstName = txtCompanyName.Text.ToString();
                customerVo.CustCode = txtCustomerCode.Text.ToString();
                customerVo.Salutation = ddlSalutation.SelectedItem.Value.ToString();
                if (customerVo.Salutation == "Mr.")
                {
                    customerVo.Gender = "M";
                }
                else
                {
                    customerVo.Gender = "F";
                }
                if (txtDateofRegistration.Text == "")
                {
                    customerVo.RegistrationDate = DateTime.MinValue;
                }
                else
                {
                    customerVo.RegistrationDate = DateTime.Parse(txtDateofRegistration.Text.ToString());
                }
                if (txtDateofCommencement.Text == "")
                {
                    customerVo.CommencementDate = DateTime.MinValue;
                }
                else
                {
                    customerVo.CommencementDate = DateTime.Parse(txtDateofCommencement.Text.ToString());
                }
                if (txtDateofProfiling.Text != "")
                {
                    customerVo.ProfilingDate = DateTime.Parse(txtDateofProfiling.Text.ToString());
                }
                else
                {
                    customerVo.ProfilingDate = DateTime.Today;
                }
                customerVo.RegistrationPlace = txtRegistrationPlace.Text.ToString();
                customerVo.CompanyWebsite = txtCompanyWebsite.Text.ToString();
                customerVo.PANNum = txtPanNumber.Text.ToString();
                customerVo.RegistrationNum = txtRocRegistration.Text.ToString();
                customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                if (txtCorrAdrPinCode.Text != "")
                {
                    customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                }
                else
                {
                    customerVo.Adr1PinCode = 0;
                }
                customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                if (ddlCorrAdrState.SelectedIndex != 0)
                {
                    customerVo.Adr1State = ddlCorrAdrState.SelectedItem.Value.ToString();
                }
                customerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
                customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                if (chkdummypan.Checked)
                {
                    customerVo.DummyPAN = 1;
                }
                else
                {
                    customerVo.DummyPAN = 0;
                }
                if (chkprospectn.Checked)
                {
                    customerVo.IsProspect = 1;
                }
                else
                {
                    customerVo.IsProspect = 0;
                }
                if (chkmailn.Checked)
                {
                    customerVo.AlertViaEmail = 1;
                }
                else
                {
                    customerVo.AlertViaEmail = 0;
                }
                if (chksmsn.Checked)
                {
                    customerVo.ViaSMS = 1;
                }
                else
                {
                    customerVo.ViaSMS = 0;
                }
                if (txtPermAdrPinCode.Text != "")
                {
                    customerVo.Adr2PinCode = int.Parse(txtPermAdrPinCode.Text.ToString());
                }
                else
                {
                    customerVo.Adr2PinCode = 0;
                }
                customerVo.Adr2City = txtPermAdrCity.Text.ToString();
                if (ddlPermAdrState.SelectedIndex != 0)
                {
                    customerVo.Adr2State = ddlPermAdrState.SelectedItem.Value.ToString();
                }
                customerVo.Adr2Country = ddlPermAdrCountry.Text.ToString();
                if (txtPhoneNo1Isd.Text != "")
                {
                    customerVo.ResISDCode = int.Parse(txtPhoneNo1Isd.Text.ToString());
                }
                else
                {
                    customerVo.ResISDCode = 0;
                }
                if (txtPhoneNo1Std.Text != "")
                {
                    customerVo.ResSTDCode = int.Parse(txtPhoneNo1Std.Text.ToString());
                }
                else
                {
                    customerVo.ResSTDCode = 0;
                }
                if (txtPhoneNo1.Text != "")
                {
                    customerVo.ResPhoneNum = int.Parse(txtPhoneNo1.Text.ToString());
                }
                else
                {
                    customerVo.ResPhoneNum = 0;
                }
                if (txtPhoneNo2Isd.Text != "")
                {
                    customerVo.OfcISDCode = int.Parse(txtPhoneNo2Isd.Text.ToString());
                }
                else
                {
                    customerVo.OfcISDCode = 0;
                }
                if (txtPhoneNo2Std.Text != "")
                {
                    customerVo.OfcSTDCode = int.Parse(txtPhoneNo2Std.Text.ToString());
                }
                else
                {
                    customerVo.OfcSTDCode = 0;
                }
                if (txtPhoneNo2.Text != "")
                {
                    customerVo.OfcPhoneNum = int.Parse(txtPhoneNo2.Text.ToString());
                }
                else
                {
                    customerVo.OfcPhoneNum = 0;
                }
                if (txtFax.Text != "")
                {
                    customerVo.Fax = int.Parse(txtFax.Text.ToString());
                }
                else
                {
                    customerVo.Fax = 0;
                }
                if (txtFaxIsd.Text != "")
                {
                    customerVo.ISDFax = int.Parse(txtFaxIsd.Text.ToString());
                }
                else
                {
                    customerVo.ISDFax = 0;
                }
                if (txtFaxStd.Text != "")
                {
                    customerVo.STDFax = int.Parse(txtFaxStd.Text.ToString());
                }
                else
                {
                    customerVo.STDFax = 0;
                }
                customerVo.Email = txtEmail.Text.ToString();
                customerVo.OfcFax = 0;
                customerVo.OfcISDFax = 0;
                customerVo.OfcSTDFax = 0;
                customerVo.OfcFax = 0;
                customerVo.OfcAdrPinCode = 0;
                customerVo.Dob = DateTime.MinValue;
                customerVo.MaritalStatus = null;
                customerVo.Nationality = null;
                customerVo.Occupation = null;
                customerVo.Qualification = null;


                if (customerBo.UpdateCustomer(customerVo))
                {
                    customerVo = customerBo.GetCustomer(customerVo.CustomerId);
                    Session["CustomerVo"] = customerVo;
                    if (customerVo.Type.ToUpper().ToString() == "IND")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewNonIndividualProfile','none');", true);
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
                FunctionInfo.Add("Method", "EditCustomerNonIndividualProfile.ascx:btnEdit_Click()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            ddlCustomerSubType.DataSource = null;
            dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
            ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataTextField = "CustomerTypeName";
            ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            if (customerVo != null)
            {
                customerVo.Type = "IND";
                Session[SessionContents.CustomerVo] = customerVo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageLoadScript", "loadcontrol('EditCustomerIndividualProfile','none');", true);
            }
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
            ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataTextField = "CustomerTypeName";
            ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            if (customerVo != null)
            {
                Session[SessionContents.CustomerVo] = customerVo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageLoadScript", "loadcontrol('EditCustomerNonIndividualProfile','none');", true);
            }
        }
        private void BindListBranch(int Id, string userType)
        {
            UploadCommonBo uploadCommonBo = new UploadCommonBo();
            DataSet ds = uploadCommonBo.GetAdviserBranchList(Id, userType);

            ddlAdviserBranchList.DataSource = ds.Tables[0];
            ddlAdviserBranchList.DataTextField = "AB_BranchName";
            ddlAdviserBranchList.DataValueField = "AB_BranchId";
            ddlAdviserBranchList.DataBind();
            ddlAdviserBranchList.Items.Insert(0, new ListItem("Select a Branch", "Select a Branch"));
        }

        protected void txtCorrAdrLine1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}