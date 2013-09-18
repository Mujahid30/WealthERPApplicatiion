using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BOAssociates;
using BoCommon;
using System.Data;

namespace WealthERP.General
{
    public partial class SetTheme : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AssociatesBo associatesBo = new AssociatesBo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsChannelList = new DataSet();
        DataSet dsTitleList = new DataSet();
        DataSet dsSubBrokerList = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            //if (Session["Theme"] != null)
            //{
            //    ddlTheme.SelectedValue = Session["Theme"].ToString();
            //}

              if (!IsPostBack)
            {
                trSales.Visible = false;
                trRM.Visible = false;
                trBranch.Visible = false;
              //  BindChannelList(advisorVo.advisorId);
            }
            
        }
         protected void BindChannelList(int adviserId)
        {            
            dsChannelList = associatesBo.BindChannelList(adviserId);
            if (dsChannelList != null)
            {
                ddlChannelList.DataSource = dsChannelList.Tables[0];
                ddlChannelList.DataTextField = "AH_ChannelName";
                ddlChannelList.DataValueField = "AH_ChannelId";
                ddlChannelList.DataBind();
            }
            ddlChannelList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Channels", "0"));
        }
        
        protected void ddlChannelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlChannelList.SelectedIndex > 0)
            //{
            //    dsTitleList = associatesBo.BindTitlesList(int.Parse(ddlChannelList.SelectedValue),advisorVo.advisorId);

            //    ddlTitlesList.DataSource = dsTitleList.Tables[0];
            //    ddlTitlesList.DataTextField = "AH_HierarchyName";
            //    ddlTitlesList.DataValueField = "AH_TitleId";
            //    ddlTitlesList.DataBind();
            //}
            //ddlTitlesList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Titles", "0"));


            BindSubbrokerList(int.Parse(ddlChannelList.SelectedValue), "Channel");
        }

        protected void ddlTitlesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTitlesList.SelectedIndex > 0)
            BindSubbrokerList(int.Parse(ddlTitlesList.SelectedValue), "Title");            
        }

        protected void ddlTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTeamList.SelectedIndex > 0)
            {
                if (ddlTeamList.SelectedValue == "Sales")
                {
                    trBranch.Visible = false;
                    trRM.Visible = false;
                    trSales.Visible = true;



                    tdChannelList.Visible = false;
                    tdTitleList.Visible = false;

                    tdChannelListDDL.Visible = false;
                    tdTitleListDDL.Visible = false;
                    tdChannelListDDL.Visible = false;
                    tdTitleListDDL.Visible = false;
                    tdSubBrokerCodeList.Visible = false;
                    tdSubBrokerCodeListDDL.Visible = false;


                }
                else if (ddlTeamList.SelectedValue == "RM")
                {
                    trBranch.Visible = false;
                    trRM.Visible = true;
                    trSales.Visible = false;
                }
                else if (ddlTeamList.SelectedValue == "Branch")
                {
                    trBranch.Visible = true;
                    trRM.Visible = false;
                    trSales.Visible = false;
                }                
            }           
        }

        protected void ddlSelectChannelOrTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectChannelOrTitle.SelectedValue == "Channel")
            {
                tdSubBrokerCodeList.Visible = true;
                tdSubBrokerCodeListDDL.Visible = true;

                tdChannelList.Visible = true;
                tdTitleList.Visible = false;

                tdChannelListDDL.Visible = true;
                tdTitleListDDL.Visible = false;

                BindChannelList(advisorVo.advisorId);          

            }
            else if (ddlSelectChannelOrTitle.SelectedValue == "Title")
            {
                tdSubBrokerCodeList.Visible = true;
                tdSubBrokerCodeListDDL.Visible = true;
                tdChannelList.Visible = false;
                tdTitleList.Visible = true;

                tdChannelListDDL.Visible = false;
                tdTitleListDDL.Visible = true;
                BindTitleList();                
                
            }
            else if (ddlSelectChannelOrTitle.SelectedValue == "0")
            {
                tdSubBrokerCodeList.Visible = false;
                tdSubBrokerCodeListDDL.Visible = false;
                tdChannelList.Visible = false;
                tdTitleList.Visible = false;

                tdChannelListDDL.Visible = false;
                tdTitleListDDL.Visible = false;
            }

            if(ddlSelectChannelOrTitle.SelectedValue != "0")
            BindSubbrokerList(0, ddlSelectChannelOrTitle.SelectedValue);
        }
        protected void BindSubbrokerList(int searchId,string searchType)
        {
            dsSubBrokerList = associatesBo.BindSubBrokerList(searchId, advisorVo.advisorId, searchType);

            if (dsSubBrokerList != null)
            {
                ddlSubBrokerCodeList.DataSource = dsSubBrokerList.Tables[0];
                ddlSubBrokerCodeList.DataTextField = "SubBrokerName";
                ddlSubBrokerCodeList.DataValueField = "AAC_AgentCode";
                ddlSubBrokerCodeList.DataBind();
            }
            ddlSubBrokerCodeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }

        protected void BindTitleList()
        {
            dsTitleList = associatesBo.BindTitlesList(advisorVo.advisorId);

            ddlTitlesList.DataSource = dsTitleList.Tables[0];
            ddlTitlesList.DataTextField = "AH_HierarchyName";
            ddlTitlesList.DataValueField = "AH_TitleId";
            ddlTitlesList.DataBind();
            ddlTitlesList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Titles", "0"));

        }
        protected void ddlTheme_SelectedIndexChanged(object sender, EventArgs e)
      {
        //    if (ddlTheme.SelectedIndex != 0)
        //    {
        //        Session["Theme"] = ddlTheme.SelectedValue;
        //        userBo.UpdateUserTheme(userVo.UserId, ddlTheme.SelectedValue);
        //    }
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "window.parent.location.href = window.parent.location.href;", true);
      }
    }
}