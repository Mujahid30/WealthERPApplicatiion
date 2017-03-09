using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoUser;
using BoCommon;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Advisor
{
    public partial class AdvisorDashBoard : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                int countLOB = CountAdvisorLOB();
                int countBranch = CountAdvisorBranch();
                int countRM = CountAdvisorStaff();
                lblBranchList.Visible = false;
                lblLOBList.Visible = false;
                lblRMList.Visible = false;
                if (countLOB > 0)
                {
                    setAdvisorLOB();
                    lblLOBList.Visible = false;
                }
                else
                {
                    lblLOBList.Visible = true;
                    gvAdvisorLOB.Visible = false;
                }

                if (countBranch > 0)
                {
                    setAdvisorBranch();
                    lblBranchList.Visible = false;
                }
                else
                {
                    lblBranchList.Visible = true;
                    gvAdvisorBranch.Visible = false;
                }

                if (countRM > 0)
                {
                    setAdvisorStaff();
                    lblRMList.Visible = false;
                }
                else
                {
                    lblRMList.Visible = true;
                    gvAdvisorRm.Visible = false;
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

                FunctionInfo.Add("Method", "AdvisorDashBoard.ascx:Page_Load()");


                object[] objects = new object[3];
                objects[0] = userVo;
                objects[1] = advisorBo;
                objects[2] = advisorVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public int CountAdvisorStaff()
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            List<RMVo> advisorStaffList = null;
            int count;
            try
            {
                advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId);
                count = advisorStaffList.Count;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDashBoard.ascx:CountAdvisorStaff()");


                object[] objects = new object[2];
                objects[0] = advisorStaffBo;
                objects[1] = advisorStaffList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
            
        }

        public int CountAdvisorBranch()
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            List<AdvisorBranchVo> advisorBranchList = null;
            int count;
            try
            {

                advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId, "");
                count = advisorBranchList.Count;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDashBoard.ascx:CountAdvisorBranch()");


                object[] objects = new object[2];
                objects[0] = advisorBranchBo;
                objects[1] = advisorBranchList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
        }

        public int CountAdvisorLOB()
        {
       //  advisorVo = (AdvisorVo)Session["advisorVo"];

            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            DataSet dsAdvisorLOBList = null;
            int count;
            try
            {
                dsAdvisorLOBList = advisorLOBBo.GetAdvisorLOBs(advisorVo.advisorId, null,null);
                count = dsAdvisorLOBList.Tables[0].Rows.Count;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDashBoard.ascx:CountAdvisorLOB()");


                object[] objects = new object[2];
                objects[0] = advisorLOBBo;
                objects[1] = dsAdvisorLOBList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
        }

        public void  setAdvisorLOB()
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];         
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            DataSet dsAdvisorLOBList = null;
            AdvisorLOBVo advisorLOBVo = null;
            
            string path;
            try
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                dsAdvisorLOBList = advisorLOBBo.GetAdvisorLOBs(advisorVo.advisorId, null, null);
                //DataTable dtAdvisorLOB = new DataTable();
                //dtAdvisorLOB.Columns.Add("SI.No");
                //dtAdvisorLOB.Columns.Add("Organization Name");
                //dtAdvisorLOB.Columns.Add("Business Type");

                //DataRow drAdvisorLOB;

                //for (int i = 0; i < dsAdvisorLOBList.Count; i++)
                //{
                //    drAdvisorLOB = dtAdvisorLOB.NewRow();
                //    advisorLOBVo = new AdvisorLOBVo();
                //    advisorLOBVo = dsAdvisorLOBList[i];
                //    drAdvisorLOB[0] = (i + 1).ToString();
                //    drAdvisorLOB[1] = advisorLOBVo.OrganizationName.ToString();
                //    drAdvisorLOB[2] = XMLBo.GetLOBType(path, advisorLOBVo.LOBClassificationCode.ToString());

                //    dtAdvisorLOB.Rows.Add(drAdvisorLOB);
                //}
                if (dsAdvisorLOBList.Tables[0].Rows.Count > 0)
                {
                    gvAdvisorLOB.DataSource = dsAdvisorLOBList.Tables[0];
                    gvAdvisorLOB.DataBind();
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

                FunctionInfo.Add("Method", "AdvisorDashBoard.ascx:setAdvisorLOB()");


                object[] objects = new object[4];
                objects[0] = advisorLOBBo;
                objects[1] = dsAdvisorLOBList;
                objects[2] = advisorVo;
                objects[3] = advisorLOBVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            
        }

        public void setAdvisorBranch()
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            List<AdvisorBranchVo> advisorBranchList = null;
            AdvisorBranchVo advisorBranchVo=null;
            try
            {
                advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId,"");
                DataTable dtAdvisorBranch = new DataTable();
                dtAdvisorBranch.Columns.Add("Sl.No.");

                dtAdvisorBranch.Columns.Add("Branch Name");
                dtAdvisorBranch.Columns.Add("Email");


                DataRow drAdvisorBranch;
                for (int i = 0; i < advisorBranchList.Count; i++)
                {
                    drAdvisorBranch = dtAdvisorBranch.NewRow();
                    advisorBranchVo = new AdvisorBranchVo();
                    advisorBranchVo = advisorBranchList[i];
                    drAdvisorBranch[0] = (i + 1).ToString();
                    drAdvisorBranch[1] = advisorBranchVo.BranchName;
                    drAdvisorBranch[2] = advisorBranchVo.Email;
                    dtAdvisorBranch.Rows.Add(drAdvisorBranch);
                }

                gvAdvisorBranch.DataSource = dtAdvisorBranch;
                gvAdvisorBranch.DataBind();
                gvAdvisorBranch.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDashBoard.ascx:setAdvisorLOB()");


                object[] objects = new object[4];
                objects[0] = advisorBranchBo;
                objects[1] = advisorBranchList;
                objects[2] = advisorVo;
                objects[3] = advisorBranchVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
       
        public void setAdvisorStaff()
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            List<RMVo> advisorStaffList = null;
            RMVo rmVo=null;
            try
            {
                advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId);
                DataTable dtAdvisorStaff = new DataTable();
                dtAdvisorStaff.Columns.Add("Sl.No.");
                dtAdvisorStaff.Columns.Add("RMName");
                dtAdvisorStaff.Columns.Add("Email");
                DataRow drAdvisorStaff;
                for (int i = 0; i < advisorStaffList.Count; i++)
                {
                    drAdvisorStaff = dtAdvisorStaff.NewRow();
                    rmVo = new RMVo();

                    rmVo = advisorStaffList[i];
                    drAdvisorStaff[0] = (i + 1).ToString();
                    drAdvisorStaff[1] = rmVo.FirstName.ToString() + rmVo.MiddleName.ToString() + rmVo.LastName.ToString();
                    drAdvisorStaff[2] = rmVo.Email.ToString();
                    dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                }
                dtAdvisorStaff.Columns[1].ColumnMapping = MappingType.Hidden;
                gvAdvisorRm.DataSource = dtAdvisorStaff;
                gvAdvisorRm.DataBind();
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDashBoard.ascx:setAdvisorStaff()");


                object[] objects = new object[4];
                objects[0] = advisorStaffBo;
                objects[1] = advisorStaffList;
                objects[2] = advisorVo;
                objects[3] = rmVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvAdvisorLOB_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdvisorLOB.PageIndex = e.NewPageIndex;
            gvAdvisorLOB.DataBind();
        }

        protected void gvAdvisorBranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdvisorBranch.PageIndex = e.NewPageIndex;
            gvAdvisorBranch.DataBind();
        }

        protected void gvAdvisorRm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdvisorRm.PageIndex = e.NewPageIndex;
            gvAdvisorRm.DataBind();
        }

    }
}