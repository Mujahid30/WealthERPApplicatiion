using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WealthERP.Base;
using BoCustomerProfiling;
using VoUser;
using System.Data;
using BoFPSuperlite;

namespace WealthERP.FP
{
    public partial class ProspectList : System.Web.UI.UserControl
    {
        CustomerBo customerbo = new CustomerBo();
        DataSet dsGetAllProspectCustomersForRM = new DataSet();
        int rmId = 0;
        CustomerProspectBo customerProspectBo = new CustomerProspectBo();
        double sum = 0;
        string clientID;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            rmId = rmVo.RMId;
            BindCustomerProspectGrid(rmId);
             
            //SqlDataSource1.SelectParameters["AR_RMId"].DefaultValue = rmVo.RMId.ToString();
        }

        private void BindCustomerProspectGrid(int rmId)
        {
            DataTable dtcustomerProspect = new DataTable();
            dsGetAllProspectCustomersForRM = customerProspectBo.GetAllProspectCustomersForRM(rmId);
            if ((dsGetAllProspectCustomersForRM.Tables[0].Rows.Count > 0) && (!string.IsNullOrEmpty(dsGetAllProspectCustomersForRM.ToString())))
            {
                gvCustomerProspectlist.Visible = true;

                dtcustomerProspect.Columns.Add("C_CustomerId");
                dtcustomerProspect.Columns.Add("Name");
                dtcustomerProspect.Columns.Add("IsProspect");
                dtcustomerProspect.Columns.Add("C_Email");
                dtcustomerProspect.Columns.Add("C_Mobile1");
                dtcustomerProspect.Columns.Add("Address");
                dtcustomerProspect.Columns.Add("Asset");
                dtcustomerProspect.Columns.Add("Liabilities");
                dtcustomerProspect.Columns.Add("Networth");
                DataRow drCustomerProspect;

                for (int i = 0; i < dsGetAllProspectCustomersForRM.Tables[0].Rows.Count; i++)
                {

                    drCustomerProspect = dtcustomerProspect.NewRow();
                    drCustomerProspect[0] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["C_CustomerId"].ToString();
                    drCustomerProspect[1] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Name"].ToString();

                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["IsProspect"].ToString() != "")
                        drCustomerProspect[2] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["IsProspect"].ToString();
                        
                    drCustomerProspect[3] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["C_Email"].ToString();
                    drCustomerProspect[4] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["C_Mobile1"].ToString();
                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Address"].ToString() != ",,,,,,")
                        drCustomerProspect[5] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Address"].ToString();
                    else
                        drCustomerProspect[5] = " ";
                        
                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Asset"].ToString() != "")
                        drCustomerProspect[6] = String.Format("{0:n2}", decimal.Parse(dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Asset"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    else
                        drCustomerProspect[6] = string.Empty;
                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Liabilities"].ToString() != "")
                        drCustomerProspect[7] = String.Format("{0:n2}", decimal.Parse(dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Liabilities"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    else
                        drCustomerProspect[7] = string.Empty;
                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Networth"].ToString() != "")
                        drCustomerProspect[8] = String.Format("{0:n2}", decimal.Parse(dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Networth"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    else
                        drCustomerProspect[8] = string.Empty;

                    dtcustomerProspect.Rows.Add(drCustomerProspect);
                }
                gvCustomerProspectlist.DataSource = dtcustomerProspect;
                gvCustomerProspectlist.DataBind();
            }
            else
            {
                gvCustomerProspectlist.Visible = false;
            }
        }

        protected void lnkbtnGvProspectListName_Click(object sender, EventArgs e)
        {
            int customerId = 0;
            int selectedRow = 0;
            GridDataItem gdi;
            CustomerVo customervo = new CustomerVo();
            CustomerBo customerBo = new CustomerBo();

            
                //LinkButton lnkbtn = (LinkButton)gvCustomerProspectlist.FindControl("lnkbtnGvProspectListName");
                LinkButton lnkbtn = (LinkButton)sender;
                gdi = (GridDataItem)lnkbtn.NamingContainer;
                selectedRow = gdi.ItemIndex + 1;
                customerId = int.Parse((gvCustomerProspectlist.MasterTableView.DataKeyValues[selectedRow-1]["C_CustomerId"].ToString()));
                customervo = customerBo.GetCustomer(customerId);
                Session["CustomerVo"] = customervo;
                
                Session["IsDashboard"] = "FP";
                if (customerId != 0)
                {
                    Session[SessionContents.FPS_ProspectList_CustomerId] = customerId;
                }
                Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
                Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPDashBoard','login');", true);
        }



        //protected void gvCustomerProspectlist_ItemDataBound(object sender, GridItemEventArgs e)
        //{
            

        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem dataItem = (GridDataItem)e.Item;
        //        Label lblAsset = (Label)(dataItem["Asset"].FindControl("lblAsset"));
        //        if (lblAsset.Text != "")
        //            sum += double.Parse(lblAsset.Text);
        //        else
        //            sum += 0;
        //    }
        //    else if (e.Item is GridFooterItem)
        //    {
        //        GridFooterItem footer = (GridFooterItem)e.Item;
        //        Label lblFooter = (Label)(footer["Asset"].FindControl("lblTotalAssets"));
        //        lblFooter.Text = sum.ToString();
        //        clientID = (lblFooter).ClientID;
        //    }
        //}

        //protected void gvCustomerProspectlist_PreRender(object sender, EventArgs e)
        //{
        //    foreach (GridDataItem dataItem in gvCustomerProspectlist.MasterTableView.Items)
        //    {
        //        (dataItem["Asset"].FindControl("lblAsset") as Label).Attributes.Add("onblur", "update('" + clientID + "'" + "," + "'" + (dataItem["Asset"].FindControl("lblAsset") as Label).ClientID + "')");
        //        (dataItem["Asset"].FindControl("lblAsset") as Label).Attributes.Add("onfocus", "getInitialValue('" + (dataItem["Asset"].FindControl("lblAsset") as Label).ClientID + "')");
        //    }
        //}  

        //protected void ddlProspectList_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    int customerId = 0;
        //    int selectedRow = 0;
        //    GridDataItem gdi;
        //    CustomerVo customervo = new CustomerVo();
        //    CustomerBo customerBo = new CustomerBo();
        //    try
        //    {
        //        RadComboBox rcb = (RadComboBox)sender;
        //        gdi = (GridDataItem)rcb.NamingContainer;
        //        selectedRow = gdi.ItemIndex;
        //        customerId = int.Parse((gvCustomerProspectlist.MasterTableView.DataKeyValues[selectedRow]["C_CustomerId"].ToString()));
        //        customervo = customerBo.GetCustomer(customerId);
                
        //        Session[SessionContents.CustomerVo] = customervo;
                
                
        //        if (customerId != 0)
        //        {
        //            Session[SessionContents.FPS_ProspectList_CustomerId] = customerId;
        //        }                
        //        if (e.Value == "ViewProfile")
        //        {
        //            Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
        //        }
        //        if (e.Value == "FinancialPlanning")
        //        {
        //            Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
        //            Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
        //            Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPDashBoard','login');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;

        //    }
        //}

        
    }
}
