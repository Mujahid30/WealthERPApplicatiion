using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using WealthERP.Base;
using BoCommon;
using System;

namespace WealthERP.Advisor
{
    public partial class LeadManagement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindLeadsGridview();
        }
        private void BindLeadsGridview()
        {
            try
            {
                lblMsg.Visible = false;
                DataTable dt = new DataTable();
                dt.Columns.Add("LeadsId", Type.GetType("System.Int32"));
                dt.Columns.Add("LeadName", Type.GetType("System.String"));
                dt.Columns.Add("Status", Type.GetType("System.String"));
                dt.Columns.Add("Product", Type.GetType("System.String"));
                dt.Columns.Add("Amount", Type.GetType("System.String"));
                dt.Columns.Add("CaptureDate", Type.GetType("System.String"));
                dt.Columns.Add("PointsEarned", Type.GetType("System.String"));

                DataRow dr;
                dr = dt.NewRow();
                dr["LeadsId"] = "111";
                dr["LeadName"] = "Harsh Mehta";
                dr["Status"] = "In Process";
                dr["Product"] = "Mutual Fund";
                dr["Amount"] = "500000";
                dr["CaptureDate"] = "09/12/2009";
                dr["PointsEarned"] = "0";
                dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["LeadsId"] = "222";
                //dr["LeadName"] = "National Insurance Co.";
                //dr["Status"] = "House Hold Articles";
                //dr["Product"] = "250000.00";
                //dr["Amount"] = "1958.00";
                //dr["CaptureDate"] = "01/01/2010";
                //dr["PointsEarned"] = "31/12/2010";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["LeadsId"] = "333";
                //dr["LeadName"] = "Reliance General Insurance Co.";
                //dr["Status"] = "Medical Critical Illness";
                //dr["Product"] = "400000.00";
                //dr["Amount"] = "6450.00";
                //dr["CaptureDate"] = "14/01/2009";
                //dr["PointsEarned"] = "13/01/2010";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["LeadsId"] = "444";
                //dr["LeadName"] = "Oriental General Insurance Co.";
                //dr["Status"] = "Travel";
                //dr["Product"] = "100000.00";
                //dr["Amount"] = "19823.00";
                //dr["CaptureDate"] = "01/05/2010";
                //dr["PointsEarned"] = "31/08/2010";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["LeadsId"] = "555";
                //dr["LeadName"] = "Bajaj Alliance Insurance Co.";
                //dr["Status"] = "Vehicle";
                //dr["Product"] = "400000.00";
                //dr["Amount"] = "21533.00";
                //dr["CaptureDate"] = "01/04/2009";
                //dr["PointsEarned"] = "31/03/2010";
                //dt.Rows.Add(dr);

                gvrLeads.DataSource = dt;
                gvrLeads.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:LoadGridview()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DropDownList ddlAction = (DropDownList)sender;
                //GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                //int selectedRow = gvr.RowIndex;
                //int LeadsId = int.Parse(gvrGeneralInsurance.DataKeys[selectedRow].Value.ToString());

                //// Set the VO into the Session
                //insuranceVo = insuranceBo.GetInsuranceAsset(LeadsId);
                //Session["insuranceVo"] = insuranceVo;
                //Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

                //if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                //{
                //    Session.Remove("table");
                //    Session.Remove("moneyBackEpisodeList");
                //    Session.Remove("insuranceULIPList");
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','?action=edit');", true);
                //}
                //if (ddlAction.SelectedItem.Value.ToString() == "View")
                //{
                //    Session.Remove("table");
                //    Session.Remove("moneyBackEpisodeList");
                //    Session.Remove("insuranceULIPList");
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','?action=view');", true);
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
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}