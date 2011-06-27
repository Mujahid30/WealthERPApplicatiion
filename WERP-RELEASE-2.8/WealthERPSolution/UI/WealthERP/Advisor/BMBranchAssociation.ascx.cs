using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class BMBranchAssociation : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        List<AdvisorBranchVo> branchList = null;
        AdvisorBranchVo advisorBranchVo = null;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        List<AdvisorBranchVo> newBranchList = new List<AdvisorBranchVo>();
        RMVo rmVo = new RMVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["rmUserVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                lblIllegal.Visible = false;
                lnkAddBranch.Visible = false;

                if (userVo.UserType == "Branch Man")
                {
                    SetBranches();
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
                FunctionInfo.Add("Method", "BMBranchAssociation.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = userVo;
                objects[1] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void SetBranches()
        {
            bool bResult = false;
            try
            {
                branchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId,"");
                for (int i = 0; i < branchList.Count; i++)
                {
                    advisorBranchVo = new AdvisorBranchVo();
                    advisorBranchVo = branchList[i];
                    bResult = advisorBranchBo.ChkBranchManagerAvail(advisorBranchVo.BranchId);
                    if (bResult)
                    {
                        newBranchList.Add(advisorBranchVo);
                    }

                }

                if (newBranchList.Count == 0)
                {
                    lblIllegal.Visible = true;
                    lnkAddBranch.Visible = true;
                    btnAssociateBM.Visible = false;
                }
                else
                {
                    lblIllegal.Visible = false;
                    lnkAddBranch.Visible = false;
                    btnAssociateBM.Visible = true;
                    DataTable dtAdvisorBranch = new DataTable();
                    dtAdvisorBranch.Columns.Add("Sl.No.");
                    dtAdvisorBranch.Columns.Add("BranchId");
                    dtAdvisorBranch.Columns.Add("Branch Name");
                    dtAdvisorBranch.Columns.Add("Branch Address");
                    dtAdvisorBranch.Columns.Add("Branch Phone");
                    DataRow drAdvisorBranch;
                    for (int i = 0; i < newBranchList.Count; i++)
                    {
                        drAdvisorBranch = dtAdvisorBranch.NewRow();
                        advisorBranchVo = new AdvisorBranchVo();
                        advisorBranchVo = newBranchList[i];
                        drAdvisorBranch[0] = (i + 1).ToString();
                        drAdvisorBranch[1] = advisorBranchVo.BranchId.ToString();
                        drAdvisorBranch[2] = advisorBranchVo.BranchName.ToString();
                        drAdvisorBranch[3] = advisorBranchVo.AddressLine1.ToString() + "'" + advisorBranchVo.AddressLine2.ToString() + "'" + advisorBranchVo.AddressLine3.ToString() + "," + advisorBranchVo.City.ToString() + "'" + advisorBranchVo.State.ToString();
                        drAdvisorBranch[4] = advisorBranchVo.Phone1Isd + "-" + advisorBranchVo.Phone1Std + "-" + advisorBranchVo.Phone1Number;
                        dtAdvisorBranch.Rows.Add(drAdvisorBranch);

                    }
                    gvBranchListBM.DataSource = dtAdvisorBranch;
                    gvBranchListBM.DataBind();
                    gvBranchListBM.Visible = true;
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
                FunctionInfo.Add("Method", "BMBranchAssociation.ascx.cs:SetBranches()");
                object[] objects = new object[3];
                objects[0] = advisorBranchVo;
                objects[1] = branchList;
                objects[2] = newBranchList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void btnAssociateBM_Click(object sender, EventArgs e)
        {
            int branchId = 0;
            try
            {
                rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                Session["rmVo"] = rmVo;

                foreach (GridViewRow gvr in this.gvBranchListBM.Rows)
                {
                    if (((RadioButton)gvr.FindControl("rbtnBM")).Checked == true)
                    {
                        branchId = int.Parse(gvBranchListBM.DataKeys[gvr.RowIndex].Value.ToString());
                        if (advisorBranchBo.AssociateBranch(rmVo.RMId, branchId,1, advisorVo.UserId))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Sorry..  Association is not done');", true);
                        }
                        // advisorBranchBo.AssociateBranch(rmVo.RMId, branchId, advisorVo.UserId);
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

                FunctionInfo.Add("Method", "BMBranchAssociation.ascx.cs:btnAssociateBM_Click()");

                object[] objects = new object[1];
                objects[0] = rmVo;
                objects[1] = branchId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {


            gvBranchListBM.SelectedIndex = -1;
            RadioButton SelectedRadioButton = (RadioButton)sender;
            GridViewRow SelectedGridRow = (GridViewRow)SelectedRadioButton.NamingContainer;
            foreach (GridViewRow GridRow in gvBranchListBM.Rows)
            {

                if (GridRow != SelectedGridRow)
                {
                    RadioButton RowRadioButton =
                         (RadioButton)GridRow.FindControl("rbtnBM");
                    RowRadioButton.Checked = false;

                }

            }
            if (SelectedRadioButton.Checked == true)
            {

                gvBranchListBM.SelectedIndex = SelectedGridRow.RowIndex;

                string MessageOutput = "Items Selected are: ";
                for (int i = 1; i < SelectedGridRow.Cells.Count; i++)
                {
                    MessageOutput += gvBranchListBM.HeaderRow.Cells[i].Text + ": " +
    SelectedGridRow.Cells[i].Text + "<br />";

                }


            }
        }

        protected void lnkAddBranch_Click(object sender, EventArgs e)
        {
            try
            {
                Session["BranchAdd"] = "forBM";
                rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                Session["rmVo"] = rmVo;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AddBranch','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "BMBranchAssociation.ascx.cs:lnkAddBranch_Click()");

                object[] objects = new object[2];
                objects[0] = rmVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

    }
}