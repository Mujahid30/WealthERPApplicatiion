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
using Telerik.Web.UI;


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
                    btnExportFilteredData.Visible = true;
                    ErrorMessage.Visible = false;
                    gvGeneralInsurance.DataSource = dt;
                    gvGeneralInsurance.DataBind();

                    if (Cache["GIList" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("GIList" + customerVo.CustomerId.ToString(), dt);
                    }
                    else
                    {
                        Cache.Remove("GIList"+ customerVo.CustomerId.ToString());
                        Cache.Insert("GIList" + customerVo.CustomerId.ToString(), dt);
                    }
                }
                else
                {
                    ErrorMessage.Visible = true;
                    btnExportFilteredData.Visible = false;
                    gvGeneralInsurance.Visible = false;
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


            RadComboBox ddlAction;
            int insuranceId;
            int accountId;
            string qryString=null;
            try
            {
                //DropDownList ddlAction = (DropDownList)sender;
                ddlAction = (RadComboBox)sender;
                //GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex+1;
                insuranceId = int.Parse(gvGeneralInsurance.MasterTableView.DataKeyValues[selectedRow - 1]["InsuranceId"].ToString());
                accountId = int.Parse(gvGeneralInsurance.MasterTableView.DataKeyValues[selectedRow - 1]["AccountId"].ToString());
                //int.TryParse(gvGeneralInsurance.MasterTableView.DataKeyValues[selectedRow - 1]["InsuranceId"].ToString(), out insuranceId);
                //int.TryParse(gvGeneralInsurance.MasterTableView.DataKeyValues[selectedRow - 1]["AccountId"].ToString(), out accountId);
                int Insurance = insuranceId;
                // insuranceId;
                

                //// Set the VO into the Session
                //insuranceVo = insuranceBo.GetInsuranceAsset(insuranceId);
                InsuranceVo insuranceVo = new InsuranceVo();

                
               
                //Session["AccountId"] = 
                
                //Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    insuranceVo.AccountId = accountId;
                    Session["insuranceId"] = insuranceId;
                    Session["AccountId"] = accountId;
                    qryString = "FromPage=ViewGeneralInsuranceDetails&InsuranceId=" + insuranceId + "&action=Edit";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceEntry','" + qryString + "');", true);
           
                }
                else if (ddlAction.SelectedItem.Value== "0")
                {
                    insuranceVo.AccountId = accountId;
                    Session["insuranceId"] = insuranceId;
                    Session["AccountId"] = accountId;
                    qryString = "FromPage=ViewGeneralInsuranceDetails&InsuranceId=" + insuranceId + "&action=View";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceEntry','" + qryString + "');", true);
           
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Delete")
                {
                    int insuranceDeleteId = int.Parse(gvGeneralInsurance.MasterTableView.DataKeyValues[selectedRow - 1]["InsuranceId"].ToString());
                    Session["insuranceDeleteId"] = insuranceDeleteId;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
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
                FunctionInfo.Add("Method", "ViewGeneralInsuranceDetails.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            int InsuranceNo = 0;
            if (val == "1")
            {
                bool DeleteAccount;
                CustomerVo customervo = (CustomerVo)Session["customerVo"];
                int Account = customervo.CustomerId ;
                if (!String.IsNullOrEmpty(Session["insuranceDeleteId"].ToString()))
                {
                    InsuranceNo = Convert.ToInt32(Session["insuranceDeleteId"].ToString());
                }
              
                

                //int Insurance = Session ;
                CustomerPortfolioBo BoCustomerPortfolio = new CustomerPortfolioBo();
                DeleteAccount = BoCustomerPortfolio.DeleteGIAccount(Account, InsuranceNo);


                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);
            }
            if (val == "0")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);
            }
        }

        protected void gvGeneralInsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvGeneralInsurance.PageIndex = e.NewPageIndex;
            BindGeneralInsuranceGridview(123);
        }

        protected void gvGeneralInsurance_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trExportFilteredData.Visible = true;
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["GIList" + customerVo.CustomerId.ToString()];
            gvGeneralInsurance.DataSource = dtGIDetails;
        }


        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            
            gvGeneralInsurance.ExportSettings.OpenInNewWindow = true;
            gvGeneralInsurance.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvGeneralInsurance.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvGeneralInsurance.MasterTableView.ExportToExcel();
        }
    }
}
