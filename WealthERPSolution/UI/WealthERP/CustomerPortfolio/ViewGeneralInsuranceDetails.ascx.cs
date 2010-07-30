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

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewGeneralInsuranceDetails : System.Web.UI.UserControl
    {
        int portfolioId = 0;
        InsuranceBo insuranceBo = new InsuranceBo();
        CustomerVo customerVo = new CustomerVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindGeneralInsuranceGridview(123);
        }

        private void BindGeneralInsuranceGridview(int portfolioId)
        {
            DataTable dt = new DataTable();
            try
            {
                #region Hard coded
                //lblMsg.Visible = false;
                //DataTable dt = new DataTable();
                //dt.Columns.Add("InsuranceId", Type.GetType("System.Int32"));
                //dt.Columns.Add("InsCompany", Type.GetType("System.String"));
                //dt.Columns.Add("Category", Type.GetType("System.String"));
                //dt.Columns.Add("SubCategory", Type.GetType("System.String"));
                //dt.Columns.Add("InsuredAmount", Type.GetType("System.String"));
                //dt.Columns.Add("PremiumAmount", Type.GetType("System.String"));
                //dt.Columns.Add("CommencementDate", Type.GetType("System.String"));
                //dt.Columns.Add("MaturityDate", Type.GetType("System.String"));

                //DataRow dr;
                //dr = dt.NewRow();
                //dr["InsuranceId"] = "111";
                //dr["InsCompany"] = "Reliance General Insurance Co.";
                //dr["Category"] = "Vehicle";
                //dr["SubCategory"] = "Car";
                //dr["InsuredAmount"] = "500000.00";
                //dr["PremiumAmount"] = "2456.00";
                //dr["CommencementDate"] = "20/11/2009";
                //dr["MaturityDate"] = "19/11/2010";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["InsuranceId"] = "222";
                //dr["InsCompany"] = "National Insurance Co.";
                //dr["Category"] = "House Hold Articles";
                //dr["SubCategory"] = "Owen";
                //dr["InsuredAmount"] = "250000.00";
                //dr["PremiumAmount"] = "1958.00";
                //dr["CommencementDate"] = "01/01/2010";
                //dr["MaturityDate"] = "31/12/2010";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["InsuranceId"] = "333";
                //dr["InsCompany"] = "Reliance General Insurance Co.";
                //dr["Category"] = "Medical Critical Illness";
                //dr["SubCategory"] = "Cancer";
                //dr["InsuredAmount"] = "400000.00";
                //dr["PremiumAmount"] = "6450.00";
                //dr["CommencementDate"] = "14/01/2009";
                //dr["MaturityDate"] = "13/01/2010";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["InsuranceId"] = "444";
                //dr["InsCompany"] = "Oriental General Insurance Co.";
                //dr["Category"] = "Travel";
                //dr["SubCategory"] = "Abroad";
                //dr["InsuredAmount"] = "100000.00";
                //dr["PremiumAmount"] = "19823.00";
                //dr["CommencementDate"] = "01/05/2010";
                //dr["MaturityDate"] = "31/08/2010";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["InsuranceId"] = "555";
                //dr["InsCompany"] = "Bajaj Alliance Insurance Co.";
                //dr["Category"] = "Vehicle";
                //dr["SubCategory"] = "Bus";
                //dr["InsuredAmount"] = "400000.00";
                //dr["PremiumAmount"] = "21533.00";
                //dr["CommencementDate"] = "01/04/2009";
                //dr["MaturityDate"] = "31/03/2010";
                //dt.Rows.Add(dr);
                #endregion

                dt = insuranceBo.GetCustomerGIDetails(customerVo.CustomerId);
                if (dt.Rows.Count > 0)
                {
                    ErrorMessage.Visible = false;
                    gvGeneralInsurance.DataSource = dt;
                    gvGeneralInsurance.DataBind();
                }
                else
                    ErrorMessage.Visible = true;

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

        //protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    portfolioId = int.Parse(ddlPortfolio.SelectedValue.ToString());
        //    Session[SessionContents.PortfolioId] = portfolioId;
        //    BindGeneralInsuranceGridview(portfolioId);
        //}

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int insuranceId;
            string qryString=null;
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int.TryParse(gvGeneralInsurance.DataKeys[selectedRow].Value.ToString(),out insuranceId);

                //// Set the VO into the Session
                //insuranceVo = insuranceBo.GetInsuranceAsset(insuranceId);
                //Session["insuranceVo"] = insuranceVo;
                //Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    qryString = "?FromPage=ViewGeneralInsuranceDetails&InsuranceId=" + insuranceId + "&action=Edit";
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    qryString = "?FromPage=ViewGeneralInsuranceDetails&InsuranceId=" + insuranceId + "&action=View";
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceEntry','" + qryString + "');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewGeneralInsuranceDetails.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvGeneralInsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGeneralInsurance.PageIndex = e.NewPageIndex;
            BindGeneralInsuranceGridview(123);
        }

    }
}