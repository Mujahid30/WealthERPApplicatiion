using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Data;
using System.Web.Services;
using System.Xml;
using System.Web.Script.Services;
using System.Xml.Linq;
using VoUser;
using BoSuperAdmin;

namespace WealthERP.SuperAdmin
{
    public partial class SuperAdminMessageBroadcast : System.Web.UI.UserControl
    {
        List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
        AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
        SuperAdminOpsBo superAdminBo = new SuperAdminOpsBo();
        protected void Page_Init(object sender, EventArgs e)
        {


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsMessage = advisermaintanencebo.GetMessageBroadcast();
            if (dsMessage != null && dsMessage.Tables[0].Rows.Count > 0)
            {
                MessageBroadcast.Visible = true;
                if (dsMessage.Tables[0].Rows[0]["ABM_IsActive"].ToString() == "1")
                {
                    DateTime dtMessageDate = DateTime.Parse(dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessageDate"].ToString());
                    lblSuperAdmnMessage.Text = "Last Sent Message:" + dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessage"].ToString() + Environment.NewLine + " Sent on:" + dtMessageDate.ToString();
                }
            }
            BindAdviserGrid();
        }

        protected void SendMessage_Click(object sender, EventArgs e)
        {
            try
            {                
                AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
                DateTime dtExpiry = new DateTime();
                bool result = false;
                string gvAdviserIds = "";

                foreach (GridViewRow gvRow in gvAdviserList.Rows)
                {
                    CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkBx");
                    if (ChkBxItem.Checked)
                    {
                        gvAdviserIds += Convert.ToString(gvAdviserList.DataKeys[gvRow.RowIndex].Value) + "~";
                    }
                }
                result = DateTime.TryParse(txtExpiryDate.Text.ToString(), out dtExpiry);
                advisermaintanencebo.MessageBroadcastSendMessage(MessageBox.Text, DateTime.Now, dtExpiry, gvAdviserIds);
                SuccessMessage.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BindAdviserGrid()
        {
            adviserVoList = advisermaintanencebo.GetAdviserList();
            DataTable dtFinalAdviserList = new DataTable();

            dtFinalAdviserList.Columns.Add("AdviserId");
            dtFinalAdviserList.Columns.Add("OrgName");            
            DataRow drAdviserList;            

            if (adviserVoList.Count > 0)
            {
                foreach (AdvisorVo advisorVo in adviserVoList)
                {
                    drAdviserList = dtFinalAdviserList.NewRow();
                    drAdviserList["AdviserId"] = advisorVo.advisorId;
                    drAdviserList["OrgName"] = advisorVo.OrganizationName;
                    dtFinalAdviserList.Rows.Add(drAdviserList);
                }
            }
            gvAdviserList.Visible = true;
            gvAdviserList.DataSource = dtFinalAdviserList;
            gvAdviserList.DataBind();
        }        
    }
}