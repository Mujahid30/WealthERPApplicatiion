using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using BoCommon;
using System.Data;
using WealthERP.Base;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Globalization;

namespace WealthERP.Advisor
{
    public partial class EditBranchDetails : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdviserAssociateCategorySetupBo adviserAssociateCategoryBo = new AdviserAssociateCategorySetupBo();
        AdvisorAssociateCommissionVo advisorAssociateCommissionVo = new AdvisorAssociateCommissionVo();
        UserBo userBo = new UserBo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        string branchId;
        string path;
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        static string firstName, middleName, lastName;

        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;
        string UploadImagePath = string.Empty;
        string imgPath = string.Empty;
        bool branchRMDependendency = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                imgPath = Server.MapPath("Images") + "\\";
                advisorBranchVo = (AdvisorBranchVo)Session["advisorBranchVo"];
                branchRMDependendency = advisorBranchBo.CheckBranchDependency(advisorBranchVo.BranchId);
                advisorVo =(AdvisorVo) Session[SessionContents.AdvisorVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                if (!IsPostBack)
                {
                    BindStates(path);
                    BindDropDowns(path);
                  
                    editBranchDetails();
                    if (ddlBranchAssociateType.SelectedValue == "1")
                    {
                        AssociateCategoryRow.Visible = false;
                        AssociateLogoRow.Visible = false;
                        AssociateLogoHdr.Visible = false;
                        CommSharingStructureGv.Visible = false;
                        CommSharingStructureHdr.Visible = false;
                       
                    }
                    else if (ddlBranchAssociateType.SelectedValue == "2")
                    {
                        AssociateCategoryRow.Visible = true;
                        AssociateLogoRow.Visible = true;
                        AssociateLogoHdr.Visible = true;
                        CommSharingStructureGv.Visible = true;
                        CommSharingStructureHdr.Visible = true;                      
                        BindCommnGridView();
 
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
                FunctionInfo.Add("Method", "EditBranchDetails.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = advisorBranchVo;               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void BindStates(string path)
        {
            DataTable dtStates = XMLBo.GetStates(path);
            ddlState.DataSource = dtStates;
            ddlState.DataValueField = "StateCode";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Select a State", "Select a State"));
        }

        //public void showRM()
        //{
        //    List<RMVo> rmList = null;
        //    RMVo rmVo = new RMVo();
        //    DataTable dt = new DataTable(); ;
        //    DataRow dr;
        //    try
        //    {
        //        ddlRmlist.Items.Clear();
        //        rmList = advisorStaffBo.GetRMList(advisorVo.advisorId);


        //        for (int i = 0; i < rmList.Count; i++)
        //        {
        //            dr = dt.NewRow();
        //            rmVo = new RMVo();
        //            ListItem rmListItem = new ListItem();
        //            rmVo = rmList[i];
        //            rmListItem.Text = rmVo.FirstName.ToString() + rmVo.LastName.ToString();
        //            rmListItem.Value = rmVo.RMId.ToString();
        //            ddlRmlist.Items.Add(rmListItem);
        //            dt.Rows.Add(dr);

        //        }
        //        ddlRmlist.Items.Insert(0, "Select Branch head");
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AddBranch.ascx:showRM()");


        //        object[] objects = new object[3];
        //        objects[0] = rmList;
        //        objects[1] = rmVo;
        //        objects[2] = advisorVo;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}

        public void showRM()
        {

            DataTable dt = new DataTable(); ;
            DataRow dr;
            int flag = 2;

            if (ddlBranchAssociateType.SelectedValue == "1")
                flag = 0;
            else if (ddlBranchAssociateType.SelectedValue == "2")
                flag = 1;
            try
            {
                ddlRmlist.Items.Clear();
                dt = advisorStaffBo.GetExternalRMList(advisorVo.advisorId, flag);
                if (dt.Rows.Count > 0)
                {
                    ddlRmlist.DataSource = dt;
                    ddlRmlist.DataValueField = "AR_RMId";
                    ddlRmlist.DataTextField = "RMName";
                    ddlRmlist.DataBind();
                }
                ddlRmlist.Items.Insert(0, "Select Branch head");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AddBranch.ascx:showRM()");


                object[] objects = new object[0];
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void editBranchDetails()
        {
            try
            {
                advisorBranchVo = (AdvisorBranchVo)Session["advisorBranchVo"];

                if(advisorBranchVo.AssociateCategoryId != 0)
                    ddlAssociateCategory.SelectedValue = advisorBranchVo.AssociateCategoryId.ToString();
                ddlBranchAssociateType.SelectedValue = advisorBranchVo.BranchTypeCode.ToString();
                if (branchRMDependendency == true)
                {
                    ddlBranchAssociateType.Enabled = false;
 
                }
                txtBranchCode.Text = advisorBranchVo.BranchCode.ToString();
                txtBranchName.Text = advisorBranchVo.BranchName.ToString();
                txtEmail.Text = advisorBranchVo.Email.ToString();
                txtFax.Text = advisorBranchVo.Fax.ToString();
                txtIsdFax.Text = advisorBranchVo.FaxIsd.ToString();
                txtIsdPhone2.Text = advisorBranchVo.Phone2Isd.ToString();
                txtIsdPhone1.Text = advisorBranchVo.Phone1Isd.ToString();
                txtLine1.Text = advisorBranchVo.AddressLine1.ToString();
                txtLine2.Text = advisorBranchVo.AddressLine2.ToString();
                txtLine3.Text = advisorBranchVo.AddressLine3.ToString();
                txtPhone1.Text = advisorBranchVo.Phone1Number.ToString();
                txtPhone2.Text = advisorBranchVo.Phone2Number.ToString();
                txtPinCode.Text = advisorBranchVo.PinCode.ToString();
                txtStdFax.Text = advisorBranchVo.FaxStd.ToString();
                txtStdPhone1.Text = advisorBranchVo.Phone1Std.ToString();
                txtStdPhone2.Text = advisorBranchVo.Phone2Std.ToString();
                txtCity.Text = advisorBranchVo.City.ToString();
                ddlCountry.Items.Clear();
                if (!string.IsNullOrEmpty(advisorBranchVo.Country.ToString().Trim()))
                    ddlCountry.Items.Add(advisorBranchVo.Country.ToString());
                else
                    ddlCountry.Items.Add("India");
                ddlState.SelectedValue = advisorBranchVo.State.ToString().Trim();
                showRM();
                ddlRmlist.SelectedValue = advisorBranchVo.BranchHeadId.ToString();
                
                txtBranchCode.Enabled = true;
                txtBranchName.Enabled = true;
                txtEmail.Enabled = true;
                txtFax.Enabled = true;
                txtIsdFax.Enabled = true;
                txtIsdPhone1.Enabled = true;
                txtIsdPhone2.Enabled = true;
                txtLine1.Enabled = true;
                txtLine2.Enabled = true;
                txtLine3.Enabled = true;
                txtPhone1.Enabled = true;
                txtPhone2.Enabled = true;
                txtPinCode.Enabled = true;
                txtStdFax.Enabled = true;
                txtStdPhone1.Enabled = true;
                txtStdPhone2.Enabled = true;
                txtCity.Enabled = true;
                ddlCountry.Enabled = true;
                ddlState.Enabled = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EditBranchDetails.ascx:editBranchDetails()");
                object[] objects = new object[2];                
                objects[1] = advisorBranchVo;
                objects[2] = branchId;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public bool chkAvailability()
        {
            bool result = false;
            string id;
            try
            {
                id = txtEmail.Text;
                result = userBo.ChkAvailability(id);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditBranchDetails.ascx:chkAvailability()");


                object[] objects = new object[1];
                objects[0] = result;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool Validation()
        {
            bool result = true;
            double res;
            
           if(!Double.TryParse(txtFax.Text,out res) || !Double.TryParse (txtIsdFax .Text ,out res) || !Double .TryParse (txtStdFax.Text ,out res))
            {
                lblFax.CssClass = "Error";
                result = false;
            }
           if (!Double.TryParse(txtStdPhone1.Text, out res) || !Double.TryParse(txtIsdPhone1.Text, out res) || !Double.TryParse(txtPhone1.Text, out res))
           {
               lblPhoneNumber.CssClass = "Error";
               result = false;
           }
           if (!Double.TryParse(txtPhone2.Text, out res) || !Double.TryParse(txtStdPhone2.Text, out res) || !Double.TryParse(txtIsdPhone2.Text, out res))
           {
               lblPhoneNumber2.CssClass = "Error";
               result = false;
           }
           
           
          if (txtPhone1.Text == "")
                {
                    lblPhoneNumber.CssClass ="Error";
                    result = false;
                }
              
                if (txtStdPhone1.Text == " ")
                {
                    lblSTD.CssClass ="Error";
                    result = false;
                }
                
                
              
            if (txtLine1.Text == "")
            {
                lblLine1.CssClass ="Error";
                result = false;
            }
           
            
            return result;
            

        }

        public bool ChkMailId(string email)
        {
            bool bResult = false;
            try
            {
                if (email == null)
                {
                    bResult = false;
                }
                int nFirstAT = email.IndexOf('@');
                int nLastAT = email.LastIndexOf('@');

                if ((nFirstAT > 0) && (nLastAT == nFirstAT) && (nFirstAT < (email.Length - 1)))
                {

                    bResult = true;
                }
                else
                {

                    bResult = false;
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

                FunctionInfo.Add("Method", "EditBranchDetails.ascx:ChkMailId()");


                object[] objects = new object[1];
                objects[0] = email;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
             AdvisorBranchVo newAdvisorBranchVo = new AdvisorBranchVo();
            try
            {
                if (Validation())
                {
                    newAdvisorBranchVo.BranchTypeCode = Int32.Parse(ddlBranchAssociateType.SelectedItem.Value.ToString());
                    if (ddlBranchAssociateType.SelectedValue.ToString() == "2")
                    {
                        if (ddlAssociateCategory.SelectedIndex!=0)
                        newAdvisorBranchVo.AssociateCategoryId = Int32.Parse(ddlAssociateCategory.SelectedItem.Value.ToString());
                        if (logoChange.HasFile)
                        {
                            newAdvisorBranchVo.LogoPath = advisorVo.advisorId + "_" + txtBranchCode.Text.ToString() + DateTime.Now.Ticks + ".jpg";
                            HttpPostedFile myFile = logoChange.PostedFile;
                            UploadImage(imgPath, myFile, newAdvisorBranchVo.LogoPath);
                            //logoChange.SaveAs(Server.MapPath("Images") + "\\" + advisorVo.advisorId + "_" + txtBranchCode.Text.ToString() + ".jpg");
                        }
                    }

                    newAdvisorBranchVo.BranchId = advisorBranchVo.BranchId;
                    newAdvisorBranchVo.AddressLine1 = txtLine1.Text.ToString();
                    newAdvisorBranchVo.AddressLine2 = txtLine2.Text.ToString();
                    newAdvisorBranchVo.AddressLine2 = txtLine2.Text.ToString();
                    newAdvisorBranchVo.AddressLine3 = txtLine3.Text.ToString();
                    newAdvisorBranchVo.BranchCode = txtBranchCode.Text.ToString();
                    newAdvisorBranchVo.BranchName = txtBranchName.Text.ToString();
                    newAdvisorBranchVo.City = txtCity.Text.Trim();
                    newAdvisorBranchVo.Country = ddlCountry.SelectedItem.Value.ToString();
                    newAdvisorBranchVo.Email = txtEmail.Text.ToString();
                    newAdvisorBranchVo.Fax = int.Parse(txtFax.Text.ToString());
                    newAdvisorBranchVo.FaxIsd = int.Parse(txtIsdFax.Text.ToString());
                    newAdvisorBranchVo.FaxStd = int.Parse(txtStdFax.Text.ToString());
                    newAdvisorBranchVo.Phone1Isd = int.Parse(txtIsdPhone1.Text.ToString());
                    newAdvisorBranchVo.Phone1Number = int.Parse(txtPhone1.Text.ToString());
                    newAdvisorBranchVo.Phone1Std = int.Parse(txtStdPhone1.Text.ToString());
                    newAdvisorBranchVo.Phone2Isd = int.Parse(txtIsdPhone2.Text.ToString());
                    newAdvisorBranchVo.Phone2Number = int.Parse(txtPhone2.Text.ToString());
                    newAdvisorBranchVo.Phone2Std = int.Parse(txtStdPhone2.Text.ToString());
                    newAdvisorBranchVo.PinCode = int.Parse(txtPinCode.Text.ToString());
                    newAdvisorBranchVo.BranchHeadId = int.Parse(ddlRmlist.SelectedItem.Value.ToString());
                   
                    if (ddlState.SelectedIndex != 0)
                    {
                        newAdvisorBranchVo.State = ddlState.SelectedValue.ToString();
                    }
                    else
                        newAdvisorBranchVo.State = "";

                    advisorBranchBo.UpdateAdvisorBranch(newAdvisorBranchVo);

                    //Add Associate Commission Details
                    if (ddlBranchAssociateType.SelectedValue.ToString() == "2")
                    {
                        foreach (GridViewRow row in gvCommStructure.Rows)
                        {

                            DropDownList ddlAssGp = (DropDownList)row.FindControl("ddlAssetGroup");
                            TextBox commfee = (TextBox)row.FindControl("txtCommFee");
                            TextBox revUpper = (TextBox)row.FindControl("txtRevUpperLimit");
                            TextBox revLower = (TextBox)row.FindControl("txtRevLowerLimit");
                            TextBox startDate = (TextBox)row.FindControl("txtStartDate");
                            TextBox endDate = (TextBox)row.FindControl("txtEndDate");

                            if (ddlAssGp.SelectedIndex != 0 && !string.IsNullOrEmpty(commfee.Text.Trim()) && !string.IsNullOrEmpty(revUpper.Text.Trim()) && !string.IsNullOrEmpty(revLower.Text.Trim()) && !string.IsNullOrEmpty(startDate.Text.Trim()) && !string.IsNullOrEmpty(endDate.Text.Trim()))
                            {
                                advisorAssociateCommissionVo.LOBAssetGroupsCode = ddlAssGp.SelectedValue.ToString();
                                if (commfee.Text.ToString() != string.Empty)
                                    advisorAssociateCommissionVo.CommissionFee = float.Parse(commfee.Text.ToString());
                                else
                                    advisorAssociateCommissionVo.CommissionFee = 0;
                                if (revUpper.Text.ToString() != string.Empty)
                                    advisorAssociateCommissionVo.RevenueUpperlimit = double.Parse(revUpper.Text.ToString());
                                else
                                    advisorAssociateCommissionVo.RevenueUpperlimit = 0;
                                if (revLower.Text.ToString() != string.Empty)
                                    advisorAssociateCommissionVo.RevenueLowerlimit = double.Parse(revLower.Text.ToString());
                                else
                                    advisorAssociateCommissionVo.RevenueLowerlimit = 0;
                                if (startDate.Text.ToString() != string.Empty)
                                    advisorAssociateCommissionVo.StartDate = Convert.ToDateTime(startDate.Text.ToString());
                                else
                                    advisorAssociateCommissionVo.StartDate = DateTime.MinValue;
                                if (endDate.Text.ToString() != string.Empty)
                                    advisorAssociateCommissionVo.EndDate = Convert.ToDateTime(endDate.Text.ToString());
                                else
                                    advisorAssociateCommissionVo.EndDate = DateTime.MinValue;
                                advisorAssociateCommissionVo.BranchId = advisorBranchVo.BranchId;

                                if (gvCommStructure.DataKeys[row.RowIndex].Value == DBNull.Value || gvCommStructure.DataKeys[row.RowIndex].Value == "")
                                    advisorBranchBo.AddAssociateCommission(userVo.UserId, advisorAssociateCommissionVo);
                                else
                                {
                                    advisorAssociateCommissionVo.Id = int.Parse(gvCommStructure.DataKeys[row.RowIndex].Value.ToString());
                                    advisorBranchBo.UpdateAssociateCommission(userVo.UserId, advisorAssociateCommissionVo);
                                }
                            }
                        }
                    }
                    // Creating Branch Association
                    int cnt = advisorStaffBo.CheckRMMainBranch(advisorBranchVo.BranchHeadId);
                    if (cnt == 0)
                        advisorBranchBo.AssociateBranch(newAdvisorBranchVo.BranchHeadId, advisorBranchVo.BranchId, 1, userVo.UserId);
                    else
                        advisorBranchBo.AssociateBranch(newAdvisorBranchVo.BranchHeadId, advisorBranchVo.BranchId, 0, userVo.UserId);
                    btnSaveChanges.Enabled = false;
                    if (Session["FromAdvisorView"] != null)
                    {
                        if (Session["FromAdvisorView"].ToString() == "FromAdvView")
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranchDetails','none');", true);
                        }
                        Session.Remove("FromAdvisorView");
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
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
                FunctionInfo.Add("Method", "EditBranchDetails.ascx:btnSaveChanges_Click()");
                object[] objects = new object[2];
                objects[0] = advisorBranchVo;
                objects[1] = newAdvisorBranchVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
      
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
             
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EditBranchDetails.ascx:btnDelete_Click()");
                object[] objects = new object[1];
                objects[0] = advisorBranchVo;               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
        }

        protected void ddlRmlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRmlist.SelectedIndex == 0)
            {
                return;
            }
                         
            try
            {
                int userId = advisorStaffBo.GetUserId(int.Parse(ddlRmlist.SelectedItem.Value.ToString()));
                rmVo = advisorStaffBo.GetAdvisorStaff(userId);
                Session["newRMVo"] = rmVo;               

                firstName = rmVo.FirstName.ToString();
                middleName = rmVo.MiddleName.ToString();
                lastName = rmVo.LastName.ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditBranchDetails.ascx:btnSaveChanges_Click()");


                object[] objects = new object[4];
                objects[0] = rmVo;
                objects[1] = firstName;
                objects[2] = middleName;
                objects[3] = lastName;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        
        }

        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                DeleteBranch();
            }
         
        }

        private void DeleteBranch()
        {
            bool res = false;
            bool branchCustomerDependency = false;
            advisorBranchVo = (AdvisorBranchVo)Session["advisorBranchVo"];
            branchCustomerDependency = advisorBranchBo.CheckBranchCustomerDependency(advisorBranchVo.BranchId);
            if (branchCustomerDependency == true)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry... Branch is not deleted...');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);

            }
            else
            {
                res = advisorBranchBo.DeleteBranch(advisorBranchVo.BranchId);
 
            }
            if (res == true)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Branch deleted Sucessfully');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
 
            }
      
            
        }

        protected void ddlBranchAssociateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranchAssociateType.SelectedValue == "2")
            {
                AssociateCategoryRow.Visible = true;
                AssociateLogoRow.Visible = true;
                AssociateLogoHdr.Visible = true;
                CommSharingStructureHdr.Visible = true;
                CommSharingStructureGv.Visible = true;
            }
            else
            {
                AssociateCategoryRow.Visible = false;
                AssociateLogoRow.Visible = false;
                AssociateLogoHdr.Visible = false;
                CommSharingStructureHdr.Visible = false;
                CommSharingStructureGv.Visible = false;
            }
            showRM();
        }

        private void BindCommnGridView()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = advisorBranchBo.GetBranchAssociateCommission(advisorBranchVo.BranchId);
                if (dt.Rows.Count>0)
                {
                    ViewState["CurrentTable"] = dt;
                    gvCommStructure.DataSource = dt;
                    gvCommStructure.DataBind();
                    gvCommStructure.Visible = true;

                }
                else
                {
                    SetInitialRow();
                    //gvCommStructure.DataSource = dt;
                    //gvCommStructure.DataBind();
                    gvCommStructure.Visible = true;
 
                }
                //if (dt.Rows.Count > 0)
                //{
                //    CommSharingStructureHdr.Visible = true;
                  

                //}
                //else
                //{
                //    CommSharingStructureHdr.Visible = false;
                  

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
                FunctionInfo.Add("Method", "EditBranchDetails.ascx.cs:BindCommnGridView()");
                object[] objects = new object[2];
                objects[0] = advisorBranchVo;
                objects[1] = dt;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("AACS_Id", typeof(string)));
            dt.Columns.Add(new DataColumn("AssetGroupCode", typeof(string)));
            dt.Columns.Add(new DataColumn("CommissionFee", typeof(string)));
            dt.Columns.Add(new DataColumn("RevenueUpperLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("RevenueLowerLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("StartDate", typeof(string)));
            dt.Columns.Add(new DataColumn("EndDate", typeof(string)));
            dr = dt.NewRow();
            dr["AACS_Id"] = string.Empty;
            dr["AssetGroupCode"] = string.Empty;
            dr["CommissionFee"] = string.Empty;
            dr["RevenueUpperLimit"] = string.Empty;
            dr["RevenueLowerLimit"] = string.Empty;
            dr["StartDate"] = string.Empty;
            dr["EndDate"] = string.Empty;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            gvCommStructure.DataSource = dt;
            gvCommStructure.DataBind();
        } 

        //private void AddNewRowToGrid()
        //{

        //    int rowIndex = 0;

        //    if (ViewState["CurrentTable"] != null)
        //    {

        //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

        //        DataRow drCurrentRow = null;

        //        if (dtCurrentTable.Rows.Count > 0)
        //        {

        //            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
        //            {

        //                //extract the TextBox values

        //                DropDownList ddl1 = (DropDownList)gvCommStructure.Rows[rowIndex].Cells[0].FindControl("ddlAssetGroup");
        //                TextBox box1 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[1].FindControl("txtCommFee");
        //                TextBox box2 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[2].FindControl("txtRevUpperLimit");
        //                TextBox box3 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[3].FindControl("txtRevLowerLimit");
        //                TextBox box4 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[4].FindControl("txtStartDate");
        //                TextBox box5 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[5].FindControl("txtEndDate");

        //                drCurrentRow = dtCurrentTable.NewRow();
        //                drCurrentRow["RowNumber"] = i + 1;
        //                dtCurrentTable.Rows[i - 1]["AssetGroupCode"] = ddl1.SelectedValue;
        //                dtCurrentTable.Rows[i - 1]["CommissionFee"] = box1.Text;
        //                dtCurrentTable.Rows[i - 1]["RevenueUpperLimit"] = box2.Text;
        //                dtCurrentTable.Rows[i - 1]["RevenueLowerLimit"] = box3.Text;
        //                dtCurrentTable.Rows[i - 1]["StartDate"] = box4.Text;
        //                dtCurrentTable.Rows[i - 1]["EndDate"] = box5.Text;
        //                rowIndex++;
        //            }

        //            dtCurrentTable.Rows.Add(drCurrentRow);

        //            ViewState["CurrentTable"] = dtCurrentTable;

        //            gvCommStructure.DataSource = dtCurrentTable;

        //            gvCommStructure.DataBind();

        //        }

        //    }

        //    else
        //    {

        //        Response.Write("ViewState is null");

        //    }



        //    //Set Previous Data on Postbacks

        //    SetPreviousData();

        //}


        private void AddNewRowToGrid()
        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("AACS_Id", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("AssetGroupCode", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("CommissionFee", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("RevenueUpperLimit", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("RevenueLowerLimit", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("EndDate", typeof(string)));

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        //extract the TextBox values

                        DropDownList ddl1 = (DropDownList)gvCommStructure.Rows[rowIndex].Cells[0].FindControl("ddlAssetGroup");
                        TextBox box1 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[1].FindControl("txtCommFee");
                        TextBox box2 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[2].FindControl("txtRevUpperLimit");
                        TextBox box3 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[3].FindControl("txtRevLowerLimit");
                        TextBox box4 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[4].FindControl("txtStartDate");
                        TextBox box5 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[5].FindControl("txtEndDate");
                       
                            drCurrentRow = dtCurrentTable.NewRow();
                            //drCurrentRow["RowNumber"] = i + 1;
                            drCurrentRow = dtNewTable.NewRow();
                            //drCurrentRow["RowNumber"] = i + 1;
                            drCurrentRow["AACS_Id"] = dtCurrentTable.Rows[i - 1]["AACS_Id"];
                            drCurrentRow["AssetGroupCode"] = ddl1.SelectedValue;
                            drCurrentRow["CommissionFee"] = box1.Text;
                            drCurrentRow["RevenueUpperLimit"] = box2.Text;
                            drCurrentRow["RevenueLowerLimit"] = box3.Text;
                            drCurrentRow["StartDate"] = box4.Text;
                            drCurrentRow["EndDate"] = box5.Text;
                            dtNewTable.Rows.Add(drCurrentRow);                       
                            rowIndex++;
                    }

                    drCurrentRow = dtNewTable.NewRow();
                    dtNewTable.Rows.Add(drCurrentRow);



                    ViewState["CurrentTable"] = dtNewTable;

                    gvCommStructure.DataSource = dtNewTable;
                    gvCommStructure.DataBind();

                }

            }

            else
            {

                Response.Write("ViewState is null");

            }



            //Set Previous Data on Postbacks

            SetPreviousData();

        }

        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddl1 = (DropDownList)gvCommStructure.Rows[rowIndex].Cells[0].FindControl("ddlAssetGroup");
                        TextBox box1 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[1].FindControl("txtCommFee");
                        TextBox box2 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[2].FindControl("txtRevUpperLimit");
                        TextBox box3 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[3].FindControl("txtRevLowerLimit");
                        TextBox box4 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[4].FindControl("txtStartDate");
                        TextBox box5 = (TextBox)gvCommStructure.Rows[rowIndex].Cells[5].FindControl("txtEndDate");

                        // ddl1.DataTextField = dt.Rows[i]["AssetGroup"].ToString();
                        ddl1.SelectedValue = dt.Rows[i][0].ToString();
                        box1.Text = dt.Rows[i]["CommissionFee"].ToString();
                        box2.Text = dt.Rows[i]["RevenueUpperLimit"].ToString();
                        box3.Text = dt.Rows[i]["RevenueLowerLimit"].ToString();
                        box4.Text = dt.Rows[i]["StartDate"].ToString();
                        box5.Text = dt.Rows[i]["EndDate"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void gvCommStructure_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtViewState = (DataTable)ViewState["CurrentTable"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlAssetGroup = e.Row.FindControl("ddlAssetGroup") as DropDownList;
                DataTable dt = new DataTable();
                try
                {
                    dt = advisorBranchBo.GetAdviserAssetGroups(advisorVo.advisorId);
                    ddlAssetGroup.DataSource = dt;
                    ddlAssetGroup.DataTextField = "XALAG_LOBAssetGroup";
                    ddlAssetGroup.DataValueField = "XALAG_LOBAssetGroupsCode";
                    ddlAssetGroup.DataBind();
                    ddlAssetGroup.Items.Insert(0, new ListItem("Select Asset Group", "Select Asset Group"));
                    ddlAssetGroup.SelectedValue = dtViewState.Rows[e.Row.RowIndex]["AssetGroupCode"].ToString();
                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (Exception Ex)
                {
                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();

                    FunctionInfo.Add("Method", "AddBranch.ascx:BindGridView()");

                    object[] objects = new object[0];

                    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);
                    throw exBase;
                }
            }
        }

        protected void DisplayLogoControl(object sender, EventArgs e)
        {
            if (!logoChange.Visible)
                logoChange.Visible = true;
            else
                logoChange.Visible = false;
        }

        private void BindDropDowns(string path)
        {
            DataSet ds;
            DataTable dt = XMLBo.GetStates(path);
            ddlState.DataSource = dt;
            ddlState.DataValueField = "StateCode";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

            dt = XMLBo.GetAdviserBranchType(path);
            ddlBranchAssociateType.DataSource = dt;
            ddlBranchAssociateType.DataValueField = "XABRT_BranchTypeCode";
            ddlBranchAssociateType.DataTextField = "XABRT_BranchType";
            ddlBranchAssociateType.DataBind();
            ddlBranchAssociateType.Items.Insert(0, new ListItem("Select a Type", "Select a Type"));

            ds = adviserAssociateCategoryBo.GetAdviserAssociateCategory(advisorVo.advisorId);
            dt = ds.Tables[0];
            ddlAssociateCategory.DataSource = dt;
            ddlAssociateCategory.DataValueField = "AssociateCategoryId";
            ddlAssociateCategory.DataTextField = "AssociateCategoryName";
            ddlAssociateCategory.DataBind();
            ddlAssociateCategory.Items.Insert(0, new ListItem("Select Associate Category", "Select Associate Category"));

        }

        private void UploadImage(string ImagePath, HttpPostedFile postedFile, string fileName) // Here ImagePath is actually the Image Name
        {
            try
            {
                UploadImagePath = ImagePath;

                // Get the data
                HttpPostedFile jpeg_image_upload = postedFile;

                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                original_image.Save(UploadImagePath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));

                // Calculate the new width and height
                int width = original_image.Width;
                int height = original_image.Height;
                int new_width, new_height;
                //string thumbnail_id;

                int target_width = 70;
                int target_height = 52;
                CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, fileName); // , out thumbnail_id

                File.Delete(UploadImagePath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
            }
        }

        private void CreateThumbnail(System.Drawing.Image original_image, ref System.Drawing.Bitmap final_image, ref System.Drawing.Graphics graphic, ref MemoryStream ms, HttpPostedFile jpeg_image_upload, int width, int height, int target_width, int target_height, string prefix, bool thumb, bool watermark, out int new_width, out int new_height, string fileName) // , out string thumbnail_id
        {
            int indexOfExtension = System.IO.Path.GetFileName(jpeg_image_upload.FileName).LastIndexOf(".");
            string imageFileName = System.IO.Path.GetFileName(jpeg_image_upload.FileName).Substring(0, indexOfExtension);

            float image_ratio = (float)width / (float)height;

            if (width > height)
            {
                if (image_ratio > 1.5)
                {   // If Image width is lot greater than height, then do following
                    if (width > 100 && width < 400)
                    {
                        target_width = width;
                    }
                    else if (width > 400)
                    {
                        target_width = 400;
                    }
                }
            }

            float target_ratio = (float)target_width / (float)target_height;

            if (target_ratio > image_ratio)
            {
                new_height = target_height;
                new_width = (int)Math.Floor(image_ratio * (float)target_height);
            }
            else
            {
                new_height = (int)Math.Floor((float)target_width / image_ratio);
                new_width = target_width;
            }

            new_width = new_width > target_width ? target_width : new_width;
            new_height = new_height > target_height ? target_height : new_height;

            //final_image = new System.Drawing.Bitmap(target_width, target_height);
            final_image = new System.Drawing.Bitmap(new_width, new_height);
            graphic = System.Drawing.Graphics.FromImage(final_image);
            graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), new System.Drawing.Rectangle(0, 0, new_width, new_height));
            //int paste_x = (target_width - new_width) / 2;
            //int paste_y = (target_height - new_height) / 2;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; /* new way */
            //graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);
            graphic.DrawImage(original_image, 0, 0, new_width, new_height);

            ms = new MemoryStream();
            final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveJpeg(UploadImagePath + fileName, final_image, 100); //jpeg_image_upload.FileName
            ms.Dispose();
        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
               new EncoderParameter(Encoder.Quality, (long)quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec =
               this.getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
         
    }
}