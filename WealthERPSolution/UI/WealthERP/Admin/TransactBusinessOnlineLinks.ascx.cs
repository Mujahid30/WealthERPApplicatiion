﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoCommon;
using System.Configuration;
using System.Data;
using BoCommon;
using WealthERP.Base;

namespace WealthERP.Admin
{
    public partial class TransactBusinessOnlineLinks : System.Web.UI.UserControl
    {
        AdviserOnlineTransactionAMCLinksVo AOTALVo = new AdviserOnlineTransactionAMCLinksVo();
        AdvisorBo advisorBo = new AdvisorBo();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        string path = "";
        int linkUserId = 0;
        int linkTypeId = 1;
        DataSet dsGetAdviserLinks = new DataSet();
        DataTable dtGetAdviserLinks = new DataTable();
        List<AdviserOnlineTransactionAMCLinksVo> adviserOTALink = null;
        CustomerVo customerVo = new CustomerVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

            if (!IsPostBack)
            {
                GetAdviserLinkData();
            }
        }

        private void GetAdviserLinkData()
        {
            DataTable dtBindLinks = new DataTable();
            DataRow drAdvisorLinks;

            dtBindLinks.Columns.Add("AL_LinkId");
            dtBindLinks.Columns.Add("A_AdviserId");
            dtBindLinks.Columns.Add("XLU_LinkUserCode");
            dtBindLinks.Columns.Add("XLTY_LinkTypeCode");
            dtBindLinks.Columns.Add("AL_Link");
            dtBindLinks.Columns.Add("AL_LinkImagePath");



            if (Session[SessionContents.CustomerVo] != null)
            {
                linkUserId = 2;
            }
            else
            {
                linkUserId = 1;
            }

            AOTALVo.advisorId = advisorVo.advisorId;
            AOTALVo.AMCLinkTypeCode = linkTypeId;
            AOTALVo.AMCLinkUserCode = linkUserId;

            adviserOTALink = advisorBo.GetAdviserOnlineTransactionAMCLinks(AOTALVo);

            if (adviserOTALink != null)
            {
                msgNoRecords.Visible = false;
                for (int i = 1; i <= adviserOTALink.Count; i++)
                {
                    AOTALVo = new AdviserOnlineTransactionAMCLinksVo();
                    AOTALVo = adviserOTALink[i - 1];

                    drAdvisorLinks = dtBindLinks.NewRow();

                    drAdvisorLinks[0] = AOTALVo.AMCLinkId;
                    drAdvisorLinks[1] = AOTALVo.advisorId;
                    drAdvisorLinks[2] = AOTALVo.AMCLinkUserCode;
                    drAdvisorLinks[3] = AOTALVo.AMCLinkTypeCode;
                    if (!string.IsNullOrEmpty(AOTALVo.ExternalLinkCode) && AOTALVo.ExternalLinkCode == "TPSL")
                    {
                        drAdvisorLinks[4] = AOTALVo.AMCLinks + "&txtUserID=" + customerVo.CustomerId.ToString();
                    }
                    else
                    {
                        drAdvisorLinks[4] = AOTALVo.AMCLinks; 
                    }
                    
                    drAdvisorLinks[5] = "~/Images/" + AOTALVo.AMCImagePath;

                    dtBindLinks.Rows.Add(drAdvisorLinks);
                }
                if (dtBindLinks.Rows.Count > 0)
                {
                    gvAdviserLinks.DataSource = dtBindLinks;
                    gvAdviserLinks.DataBind();
                    //HyperLink HpLink = (HyperLink)gvAdviserLinks.HeaderRow.FindControl("hlAMCLinkImages");
                    //HpLink.ImageUrl = "Images/" + dtBindLinks.;
                }
            }           
            else
            {
                gvAdviserLinks.DataSource = null;
                msgNoRecords.Visible = true;
                msgNoRecords.InnerText = "No Links available for this adviser..";
            }
        }

        protected void imgbtnLinks_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((ImageButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvAdviserLinks.DataKeys[rowIndex];
            string Link = dk.Value.ToString();
           

        }

        protected void gvAdviserLinks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GetAdviserLinkData();
            int index = 0;
            index = Convert.ToInt32(e.CommandArgument);
            int linkId = int.Parse(gvAdviserLinks.DataKeys[index].Value.ToString());
            string Link = adviserOTALink[index].AMCLinks;

            if (e.CommandName == "NavigateToLink")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + Link + "','_blank')", true);
            }

        }

        protected void gvAdviserLinks_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the contents of bound column #4 ("Item4")...
                ImageButton imgSelect = (ImageButton)e.Row.FindControl("imgbtnLinks");
                Label lblURL = (Label)e.Row.FindControl("lblURL");
                imgSelect.Attributes.Add("onClick", "javascript:CallWindow('" + lblURL.Text + "+')");
            }
        }
    }
}