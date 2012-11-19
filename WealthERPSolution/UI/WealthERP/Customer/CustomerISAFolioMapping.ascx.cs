using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VoUser;
using BoCommon;
using BoUploads;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using WealthERP.Base;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;



namespace WealthERP.Customer
{
    public partial class CustomerISAFolioMapping : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerBo customerBo = new CustomerBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                txtMember_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            }
        }

        private void BindBranchDropDown()
        {
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlMemberBranch.DataSource = ds;
                    ddlMemberBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlMemberBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlMemberBranch.DataBind();
                }
                ddlMemberBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlMemberBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemberBranch.SelectedIndex == 0)
            {
                txtMember_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            }
            else
            {
                txtMember_autoCompleteExtender.ContextKey = ddlMemberBranch.SelectedValue.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
            }


        }

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];
                lblGetPan.Text = dr["C_PANNum"].ToString();

            }
        }

        protected void ddlCustomerISAAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}