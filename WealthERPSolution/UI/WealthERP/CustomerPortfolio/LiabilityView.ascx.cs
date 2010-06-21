using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using WealthERP.Base;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class LiabilityView : System.Web.UI.UserControl
    {
        List<LiabilitiesVo> liabilitiesListVo = null;
        LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
        CustomerVo customerVo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

            if (!IsPostBack)
            {
                BindGridview();
            }
        }

        protected void BindGridview()
        {
            liabilitiesListVo = new List<LiabilitiesVo>();
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            liabilitiesListVo = liabilitiesBo.GetLiabilities(customerVo.CustomerId);
            DataTable dt = new DataTable();
            DataRow dr;
            if (liabilitiesListVo != null)
            {
                lblMsg.Visible = false;
                dt.Columns.Add("LiabilityId");
                dt.Columns.Add("Loan Type");
                dt.Columns.Add("Lender");
                dt.Columns.Add("Amount");
                dt.Columns.Add("Rate of Interest");
                for (int i = 0; i < liabilitiesListVo.Count; i++)
                {
                    dr = dt.NewRow();
                    liabilityVo = liabilitiesListVo[i];
                    dr[0] = liabilityVo.LiabilitiesId;
                    dr[1] = liabilityVo.LoanType.ToString();
                    dr[2] = liabilityVo.LoanPartner.ToString();
                    dr[3] = decimal.Parse(liabilityVo.LoanAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")); 
                    dr[4] = liabilityVo.RateOfInterest.ToString();
                    dt.Rows.Add(dr);
                }
                gvLiabilities.DataSource = dt;
                gvLiabilities.DataBind();
            }

            else
            {
                lblMsg.Visible = true;
            }




        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menu;
            int liabilityId = 0;
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            try
            {
                DropDownList MyDropDownList = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
                int selectedRow = gvr.RowIndex;
                liabilityId = int.Parse(gvLiabilities.DataKeys[selectedRow].Value.ToString());
                liabilityVo = liabilitiesBo.GetLiabilityDetails(liabilityId);
                
                menu = MyDropDownList.SelectedItem.Value.ToString();
              
                if (menu == "View")
                {
                    Session["menu"] = "View";
                    Session["liabilityVo"] = liabilityVo;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
                if (menu == "Edit")
                {
                     Session["menu"]="Edit";
                     Session["liabilityVo"] = liabilityVo;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
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
                FunctionInfo.Add("Method", "LiabilityView.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[2];
                objects[1] = liabilityVo;
                objects[2] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvLiabilities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLiabilities.PageIndex = e.NewPageIndex;
            BindGridview();
        }
    }
}