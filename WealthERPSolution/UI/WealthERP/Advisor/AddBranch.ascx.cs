using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace WealthERP.Advisor
{
    public partial class AddBranch : System.Web.UI.UserControl
    {
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdviserAssociateCategorySetupBo adviserAssociateCategoryBo = new AdviserAssociateCategorySetupBo();
        AdvisorAssociateCommissionVo advisorAssociateCommissionVo = new AdvisorAssociateCommissionVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        RMVo rmVo = new RMVo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserBo userBo = new UserBo();
        UserVo userVo = new UserVo();
        static string firstName, middleName, lastName;
        string path;
        List<RMVo> rmList = null;
        int advisorId;
        int userId;

        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;
        string UploadImagePath=string.Empty;
        string imgPath = string.Empty;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["UserVo"];
                rmVo = (RMVo)Session["rmVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                imgPath = Server.MapPath("Images") + "\\";
                if (!IsPostBack)
                {

                    //lblEmail.CssClass = "FieldName";
                    //lblLine1.CssClass = "FieldName";
                    //lblPhoneNumber.CssClass = "FieldName";
                    //lblSTD.CssClass = "FieldName";
                    //lblISD.CssClass = "FieldName";
                    //lblPinCode.CssClass = "FieldName";
                    //lblFax.CssClass = "FieldName";
                    //showRM();
                    ddlRmlist.Items.Insert(0, "Select Branch head");
                    BindDropDowns(path);
                    //BindGridView(advisorVo.advisorId, int.Parse(ddlAssociateCategory.SelectedValue.ToString()));
                    //AssociateLogoRow.Style.Add("display", "none");
                    //AssociateCategoryRow.Style.Add("display", "none");
                    //CommSharingStructureHdr.Style.Add("display", "none");
                    //CommSharingStructureGv.Style.Add("display", "none");
                    AssociateCategoryRow.Visible = false;
                    AssociateLogoRow.Visible = false;
                    AssociateLogoHdr.Visible = false;
                    CommSharingStructureGv.Visible = false;
                    CommSharingStructureHdr.Visible = false;
                    trCommsharingHeading.Visible = false; ;
                    SetInitialRow();
                    CheckAdviserEquityLob();    //Making invisible Terminal id add row if there is no Equity LOB
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

                FunctionInfo.Add("Method", "AddBranch.ascx:PageLoad()");

                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = userVo;
                objects[2] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

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

        private void BindGridView()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("AssetGroup", typeof(string)));
            dt.Columns.Add(new DataColumn("CommFee", typeof(string)));
            dt.Columns.Add(new DataColumn("RevUpperLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("RevLowerLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("StartDate", typeof(string)));
            dt.Columns.Add(new DataColumn("EndDate", typeof(string)));
            DataRow dr = null;
            dr = dt.NewRow();

            dr["CommFee"] = string.Empty;
            dr["RevUpperLimit"] = string.Empty;
            dr["RevLowerLimit"] = string.Empty;
            dr["StartDate"] = string.Empty;
            dr["EndDate"] = string.Empty;
            //TextBox box2 = (TextBox)gvAssocCatSetUp.Rows[rowIndex].Cells[2].FindControl("TextBox2");
            //box2.Text = Category + i;
            dt.Rows.Add(dr);

            //ViewState["CurrentTable"] = dt;
            gvCommStructure.DataSource = dt;
            gvCommStructure.DataBind();
        }

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
                dt = advisorStaffBo.GetExternalRMList(advisorVo.advisorId,flag);
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

        public bool chkAvailability()
        {
            bool result = false;
            string id = "";
            try
            {
                id = txtEmail.Text.Trim();
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

                FunctionInfo.Add("Method", "AddBranch.ascx:chkAvailability()");


                object[] objects = new object[2];
                objects[0] = result;
                objects[1] = id;

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
            try
            {
                if (ddlRmlist.SelectedIndex == 0)
                {
                    result = false;
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

                FunctionInfo.Add("Method", "AddBranch.ascx:Validation()");


                object[] objects = new object[1];
                objects[0] = result;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

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

                FunctionInfo.Add("Method", "AddBranch.ascx:ChkMailId()");


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
            string branchAdd = "";

            UserVo userVo = null;

            try
            {
                branchAdd = Session["BranchAdd"].ToString();
                userVo = (UserVo)Session["UserVo"];

                // if (chkAvailability())
                //{


                if (Validation())
                {
                    AdvisorBo advisorBo = new AdvisorBo();
                    advisorId = advisorVo.advisorId;
                    userId = userVo.UserId;

                    //advisorBranchVo.BranchId = advisorBo.getId();
                    advisorBranchVo.AddressLine1 = txtLine1.Text.ToString();
                    advisorBranchVo.AddressLine2 = txtLine2.Text.ToString();
                    advisorBranchVo.AddressLine3 = txtLine3.Text.ToString();
                    advisorBranchVo.BranchCode = txtBranchCode.Text.ToString();
                    rmList = advisorStaffBo.GetRMList(advisorVo.advisorId);
                    if (rmList == null)
                    {
                        advisorBranchVo.BranchHeadId = 100;
                    }
                    else
                    {
                        if (ddlRmlist.SelectedIndex != 0)
                        {
                            advisorBranchVo.BranchHeadId = int.Parse(ddlRmlist.SelectedItem.Value.ToString());
                        }
                    }
                    if (txtMobileNumber.Text == "")
                    {
                        advisorBranchVo.MobileNumber = 0;
                    }
                    else
                    {
                        advisorBranchVo.MobileNumber = long.Parse(txtMobileNumber.Text);
                    }
                    //advisorBranchVo.BranchId = advisorBo.getId().ToString();
                    advisorBranchVo.BranchName = txtBranchName.Text.ToString();
                    advisorBranchVo.City = txtCity.Text.Trim();
                    advisorBranchVo.Country = ddlCountry.SelectedItem.Value.ToString();
                    advisorBranchVo.Email = txtEmail.Text.Trim().ToString();

                    advisorBranchVo.BranchTypeCode = Int32.Parse(ddlBranchAssociateType.SelectedItem.Value.ToString());
                    if (ddlBranchAssociateType.SelectedValue.ToString() == "2")
                    {
                        if (ddlAssociateCategory.SelectedIndex != 0)
                         advisorBranchVo.AssociateCategoryId = Int32.Parse(ddlAssociateCategory.SelectedItem.Value.ToString());
                        if (FileUpload.HasFile)
                        {
                            advisorBranchVo.LogoPath = advisorVo.advisorId + "_" + txtBranchCode.Text.ToString() + ".jpg";
                            HttpPostedFile myFile = FileUpload.PostedFile;
                            UploadImage(imgPath, myFile, advisorBranchVo.LogoPath);
                            //FileUpload.SaveAs(Server.MapPath("Images") + "\\" + advisorVo.advisorId + "_" + txtBranchCode.Text.ToString() + ".jpg");
                        }
                    }

                    if (txtFax.Text == "")
                    {
                        advisorBranchVo.Fax = 0;
                    }
                    else
                    {
                        advisorBranchVo.Fax = int.Parse(txtFax.Text.ToString());
                    }
                    if (txtIsdFax.Text == "")
                    {
                        advisorBranchVo.FaxIsd = 0;
                    }
                    else
                    {
                        advisorBranchVo.FaxIsd = int.Parse(txtIsdFax.Text.ToString());
                    }
                    if (txtIsdPhone1.Text == "")
                    {
                        advisorBranchVo.Phone1Isd = 0;
                    }
                    else
                    {
                        advisorBranchVo.Phone1Isd = int.Parse(txtIsdPhone1.Text.ToString());
                    }
                    if (txtIsdPhone2.Text == "")
                    {
                        advisorBranchVo.Phone2Isd = 0;
                    }
                    else
                    {
                        advisorBranchVo.Phone2Isd = int.Parse(txtIsdPhone2.Text.ToString());
                    }
                    if (txtPhone2.Text == "")
                    {
                        advisorBranchVo.Phone2Number = 0;
                    }
                    else
                    {
                        advisorBranchVo.Phone2Number = int.Parse(txtPhone2.Text.ToString());
                    }
                    if (txtStdFax.Text == "")
                    {
                        advisorBranchVo.FaxStd = 0;
                    }
                    else
                    {
                        advisorBranchVo.FaxStd = int.Parse(txtStdFax.Text.ToString());
                    }
                    if (txtStdPhone2.Text == "")
                    {
                        advisorBranchVo.Phone2Std = 0;

                    }
                    else
                    {
                        advisorBranchVo.Phone2Std = int.Parse(txtStdPhone2.Text.ToString());
                    }

                    advisorBranchVo.Phone1Number = int.Parse(txtPhone1.Text.ToString());
                    advisorBranchVo.Phone1Std = int.Parse(txtStdPhone1.Text.ToString());
                    if (txtPinCode.Text == "")
                    {
                        advisorBranchVo.PinCode = 0;
                    }
                    else
                    {
                        advisorBranchVo.PinCode = int.Parse(txtPinCode.Text.ToString());
                    }
                    if (ddlState.SelectedIndex != 0)
                    {
                        advisorBranchVo.State = ddlState.SelectedItem.Value.ToString();
                    }
                    else
                    {
                        advisorBranchVo.State = "";
                    }

                    // Create Branch 

                    advisorBranchVo.BranchId = advisorBranchBo.CreateAdvisorBranch(advisorBranchVo, advisorId, userId);

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
                            

                            if (ddlAssGp.SelectedValue.ToString() != "Select Asset Group")
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

                                advisorBranchBo.AddAssociateCommission(userId, advisorAssociateCommissionVo);
                            }

                        }   
                    }

                    // Creating Branch Association
                    int cnt = advisorStaffBo.CheckRMMainBranch(advisorBranchVo.BranchHeadId);
                    if(cnt == 0)
                        advisorBranchBo.AssociateBranch(advisorBranchVo.BranchHeadId, advisorBranchVo.BranchId, 1, userId);
                    else
                        advisorBranchBo.AssociateBranch(advisorBranchVo.BranchHeadId, advisorBranchVo.BranchId, 0, userId);

                    //if (!advisorBranchBo.CheckBranchMgrRole(advisorBranchVo.BranchHeadId))
                    //{
                    //    int rmUserId = advisorStaffBo.GetUserId(advisorBranchVo.BranchHeadId);
                    //    userBo.CreateRoleAssociation(rmUserId, 1002);
                    //}
                    //if (branchAdd == "forAdvisor")
                    //{
                    //    rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                    //    if (advisorBranchBo.AssociateBranch(rmVo.RMId, advisorBranchVo.BranchId, 0, userId))
                    //    {
                    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
                    //    }
                    //    else
                    //    {
                    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Sorry..  Association is not done');", true);
                    //    }
                    //}
                    //if (branchAdd == "forBM" || branchAdd.Trim().ToString() == "forRM")
                    //{
                    //    if (rmList != null)
                    //    {

                    //        rmVo = (RMVo)Session["newRMVo"];

                    //        //Session["newRMVo"] = rmVo;
                    //        if (advisorBranchBo.AssociateBranch(rmVo.RMId, advisorBranchVo.BranchId, 0, userId))
                    //        {
                    //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
                    //        }
                    //        else
                    //        {
                    //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Sorry..  Association is not done');", true);
                    //        }
                    //    }

                    //}

                    // Add Terminals to the Branch

                    if (Session["terminalId"] != null)
                    {

                        List<int> terminalId = (List<int>)Session["terminalId"];
                        for (int i = 0; i < terminalId.Count; i++)
                        {
                            advisorBranchBo.AddBranchTerminal(advisorBranchVo.BranchId, terminalId[i], userId);
                        }
                    }

                    Session.Remove("terminalId");
                    Session.Remove("table");
                    Session.Remove("count");
                    btnSubmit.Enabled = false;

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Please select the Branch Head..!');", true);
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

                FunctionInfo.Add("Method", "AddBranch.ascx:btnSaveChanges_Click()");


                object[] objects = new object[4];
                objects[0] = advisorBranchVo;
                objects[1] = rmVo;
                objects[2] = advisorVo;
                objects[3] = userVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlRmlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRmlist.SelectedIndex != 0)
                {
                    int userId = advisorStaffBo.GetUserId(int.Parse(ddlRmlist.SelectedItem.Value.ToString()));
                    rmVo = advisorStaffBo.GetAdvisorStaff(userId);
                    Session["newRMVo"] = rmVo;
                    txtMobileNumber.Text = rmVo.Mobile.ToString();
                    firstName = rmVo.FirstName.ToString();
                    middleName = rmVo.MiddleName.ToString();
                    lastName = rmVo.LastName.ToString();
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

                FunctionInfo.Add("Method", "AddBranch.ascx:btnSaveChanges_Click()");


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

        protected void btnAddTerminal_Click(object sender, EventArgs e)
        {
            Session["count"] = txtTerminalCount.Text;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopUpScript", "showpopup();", true);


        }

        //protected void ddlAssociateCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    AssociateCategoryRow.Style.Add("display", "");
        //    AssociateLogoRow.Style.Add("display", "");
        //    if (ddlAssociateCategory.SelectedValue != "Select Associate Category")
        //    {
        //        BindGridView();
        //        CommSharingStructureHdr.Visible = true;
        //        CommSharingStructureGv.Visible = true;
        //    }
        //    else
        //    {
        //        CommSharingStructureHdr.Visible = false;
        //        CommSharingStructureGv.Visible = false;
        //    }
        //}

        protected void ddlBranchAssociateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranchAssociateType.SelectedIndex ==2)
            {
                trCommsharingHeading.Visible = true;
                AssociateCategoryRow.Visible = true;
                AssociateLogoRow.Visible = true;
                AssociateLogoHdr.Visible = true;
                CommSharingStructureHdr.Visible = true;
                CommSharingStructureGv.Visible = true;
                
            }
            else
            {
                trCommsharingHeading.Visible = false;
                AssociateCategoryRow.Visible = false;
                AssociateLogoRow.Visible = false;
                AssociateLogoHdr.Visible = false;
                CommSharingStructureHdr.Visible = false;
                CommSharingStructureGv.Visible = false;
            }
            showRM();
        }

        protected void gvCommStructure_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("AssetGroup", typeof(string)));
            dt.Columns.Add(new DataColumn("CommFee", typeof(string)));
            dt.Columns.Add(new DataColumn("RevUpperLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("RevLowerLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("StartDate", typeof(string)));
            dt.Columns.Add(new DataColumn("EndDate", typeof(string)));
            dr = dt.NewRow();
            dr["AssetGroup"] = string.Empty;
            dr["CommFee"] = string.Empty;
            dr["RevUpperLimit"] = string.Empty;
            dr["RevLowerLimit"] = string.Empty;
            dr["StartDate"] = string.Empty;
            dr["EndDate"] = string.Empty;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            gvCommStructure.DataSource = dt;
            gvCommStructure.DataBind();
        } 

        private void AddNewRowToGrid()
        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

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
                        dtCurrentTable.Rows[i - 1]["AssetGroup"] = ddl1.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["CommFee"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["RevUpperLimit"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["RevLowerLimit"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["StartDate"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["EndDate"] = box5.Text;
                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;



                    gvCommStructure.DataSource = dtCurrentTable;

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

        //This is to refresh the grid with existing data when a new row is added
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
                        box1.Text = dt.Rows[i]["CommFee"].ToString();
                        box2.Text = dt.Rows[i]["RevUpperLimit"].ToString();
                        box3.Text = dt.Rows[i]["RevLowerLimit"].ToString();
                        box4.Text = dt.Rows[i]["StartDate"].ToString();
                        box5.Text = dt.Rows[i]["EndDate"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        private void CheckAdviserEquityLob()
        {
            DataTable dt = new DataTable();
            int flag = 0;
            try
            {
                dt = advisorBranchBo.GetAdviserAssetGroups(advisorVo.advisorId);
                foreach(DataRow dr in dt.Rows)
                {
                    if (dr["XALAG_LOBAssetGroupsCode"].ToString() == "EQ")
                        flag = 1;
                }
                if (flag == 0)
                    trNoOfTerminals.Visible = false;
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