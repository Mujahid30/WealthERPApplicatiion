using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using WealthERP.Base;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AdviserLoanMIS : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string path;
        string userType;
        int advisorId;
        int userId;

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userType = Session["UserType"].ToString().ToLower();
                BindGrid();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLoanMIS.ascx:PageLoad()");

                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindGrid()
        {
            DataSet dsLoanMIS = new DataSet();
            DataTable dtAdviserLoanMIS = new DataTable();

            int ID = 0;

            if (userType == "adviser")
                ID = advisorVo.advisorId;
            else if (userType == "rm")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                ID = rmVo.RMId;
            }
            else if (userType == "bm")
            {
                ID = 0;
            }

            try
            {
                dsLoanMIS = adviserMFMIS.GetLoanMIS(userType,ID);

                if (dsLoanMIS.Tables[0].Rows.Count > 0)
                {
                    trMessage.Visible = false;
                    lblMessage.Visible = false;

                    dtAdviserLoanMIS.Columns.Add("LiabilitiesId");
                    dtAdviserLoanMIS.Columns.Add("CustomerName");
                    dtAdviserLoanMIS.Columns.Add("LoanType");
                    dtAdviserLoanMIS.Columns.Add("LoanAmount");
                    dtAdviserLoanMIS.Columns.Add("LenderName");
                    dtAdviserLoanMIS.Columns.Add("Stage");
                    dtAdviserLoanMIS.Columns.Add("Commission");

                    DataRow drAdvLoanMIS;

                    for (int i = 0; i < dsLoanMIS.Tables[0].Rows.Count; i++)
                    {
                        drAdvLoanMIS = dtAdviserLoanMIS.NewRow();

                        drAdvLoanMIS[0] = dsLoanMIS.Tables[0].Rows[i]["LiabilitiesId"].ToString();
                        drAdvLoanMIS[1] = dsLoanMIS.Tables[0].Rows[i]["CustomerName"].ToString();
                        drAdvLoanMIS[2] = dsLoanMIS.Tables[0].Rows[i]["LoanType"].ToString();
                        drAdvLoanMIS[3] = String.Format("{0:n2}", decimal.Parse(dsLoanMIS.Tables[0].Rows[i]["LoanAmount"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvLoanMIS[4] = dsLoanMIS.Tables[0].Rows[i]["LenderName"].ToString();
                        drAdvLoanMIS[5] = dsLoanMIS.Tables[0].Rows[i]["Stage"].ToString();
                        drAdvLoanMIS[6] = String.Format("{0:n2}", decimal.Parse(dsLoanMIS.Tables[0].Rows[i]["Commission"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtAdviserLoanMIS.Rows.Add(drAdvLoanMIS);
                    }

                    gvLoanMIS.DataSource = dtAdviserLoanMIS;
                    gvLoanMIS.DataBind();
                }
                else
                {
                    trMessage.Visible = true;
                    lblMessage.Visible = true;
                    gvLoanMIS.Visible = false;
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
                FunctionInfo.Add("Method", "AdviserLoanMIS.ascx.cs:BindGrid()");
                
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}