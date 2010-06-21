using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace WealthERP.General
{
    public partial class Dashboard : System.Web.UI.UserControl
    {
        private string m_KeyName = "";

        public string KeyName
        {
            get { return m_KeyName; }
            set { m_KeyName = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDashboard();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RenderDashboards();
        }

        protected void RenderDashboards()
        {
            BPanel.Controls.Clear();

            DashboardVo dashboardVo = (DashboardVo)Session[this.UniqueID];
            if (dashboardVo == null)
            {
                lblError.Visible = true;
                lblError.Text = "Unable to display dashboard. Contact System Administrator.";
                return;
            }

            HtmlGenericControl[] Columns = new HtmlGenericControl[dashboardVo.Columns];
            for (int i = 0; i < Columns.Length; i++)
            {
                HtmlGenericControl HGC = new HtmlGenericControl("div");
                HGC.Attributes.Add("class", "column");
                HGC.ID = "column" + i.ToString();

                BPanel.Controls.Add(HGC);

                Columns[i] = HGC;
            }

            int Count = 0;
            foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList.OrderBy(part => part.UserOrder))
            {
                if (dashboardPartVo.Visible)
                {
                    string Classname = "dragbox ";
                    Classname += (dashboardPartVo.Columns == 1) ? "onecolumn" : "twocolumn";

                    HtmlGenericControl Part = new HtmlGenericControl("div");
                    Part.Attributes.Add("class", Classname);
                    Part.ID = "Part" + dashboardPartVo.DashboardPartId.ToString();

                    HtmlGenericControl Handle = new HtmlGenericControl("h2");
                    Handle.InnerText = dashboardPartVo.Name;
                    Part.Controls.Add(Handle);

                    HtmlGenericControl Content = new HtmlGenericControl("div");
                    Content.Attributes.Add("class", "dragbox-content");
                    Part.Controls.Add(Content);

                    try
                    {
                        // if loading user control fails, do nothing
                        // will throw httpexception
                        Control C = Page.LoadControl(dashboardPartVo.ControlName);
                        C.ID = "PartControl_" + dashboardPartVo.DashboardPartId.ToString();
                        Content.Controls.Add(C);
                    }
                    catch { }

                    int Column = Count % dashboardVo.Columns;
                    Columns[Column].Controls.Add(Part);

                    Count++;
                }
            }
        }

        protected void InitDashboard()
        {
            UserVo userVo = new UserVo();
            userVo.UserId = 20565;

            DashboardBo dashboardBo = new DashboardBo();

            try
            {
                DashboardVo dashboardVo = (DashboardVo)Session[this.UniqueID];

                if (dashboardVo == null)
                {
                    dashboardVo = dashboardBo.GetUserDashboard(userVo, m_KeyName);
                    Session[this.UniqueID] = dashboardVo;
                }

                PartList.Items.Clear();
                foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList)
                {
                    PartList.Items.Add(new ListItem(dashboardPartVo.Name, dashboardPartVo.DashboardPartId.ToString(), true));
                    PartList.Items[PartList.Items.Count - 1].Selected = dashboardPartVo.Visible;
                }
            }
            catch
            {
                lblError.Visible = true;
                lblError.Text = "Unable to display dashboard. Contact System Administrator.";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DashboardVo dashboardVo = (DashboardVo)Session[this.UniqueID];

                for (int i = 0; i < PartList.Items.Count; i++)
                {
                    int DPId = int.Parse(PartList.Items[i].Value);
                    bool DPSelected = PartList.Items[i].Selected;

                    foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList)
                    {
                        if (dashboardPartVo.DashboardPartId == DPId)
                        {
                            dashboardPartVo.Visible = DPSelected;
                            break;
                        }
                    }
                }

                DashboardBo dashboardBo = new DashboardBo();
                dashboardBo.UpdateUserDashboard(dashboardVo);

                InitDashboard();
            }
            catch
            {
                lblError.Visible = true;
                lblError.Text = "Unable to save dashboard. Contact System Administrator.";
            }
        }

        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                try
                {
                    DashboardVo dashboardVo = (DashboardVo)Session[this.UniqueID];
                    string[] PartIds = SortOrder.Value.Split(',');

                    int Order = 1;
                    foreach (string PartId in PartIds)
                    {
                        int PId = int.Parse(PartId);

                        foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList)
                        {
                            if (dashboardPartVo.DashboardPartId == PId)
                            {
                                dashboardPartVo.UserOrder = Order;
                                break;
                            }
                        }

                        Order++;
                    }

                    DashboardBo dashboardBo = new DashboardBo();
                    dashboardBo.UpdateUserDashboard(dashboardVo);
                }
                catch
                {
                }
            }
        }

        public string GetParams(int dashboardPartId)
        {
            string Params = "";

            DashboardVo dashboardVo = (DashboardVo)Session[this.UniqueID];

            if (dashboardVo != null)
            {
                foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList)
                {
                    if (dashboardPartVo.DashboardPartId == dashboardPartId)
                    {
                        Params = dashboardPartVo.Params;
                        break;
                    }
                }
            }

            return Params;
        }

        public string GetUserParams(int dashboardPartId)
        {
            string Params = "";

            DashboardVo dashboardVo = (DashboardVo)Session[this.UniqueID];

            if (dashboardVo != null)
            {
                foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList)
                {
                    if (dashboardPartVo.DashboardPartId == dashboardPartId)
                    {
                        Params = dashboardPartVo.UserParams;
                        break;
                    }
                }
            }

            return Params;
        }

        public void SetUserParams(int dashboardPartId, string Params)
        {
            DashboardVo dashboardVo = (DashboardVo)Session[this.UniqueID];

            if (dashboardVo != null)
            {
                foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList)
                {
                    if (dashboardPartVo.DashboardPartId == dashboardPartId)
                    {
                        dashboardPartVo.UserParams = Params;

                        DashboardBo dashboardBo = new DashboardBo();
                        dashboardBo.UpdateUserDashboard(dashboardVo);

                        break;
                    }
                }
            }
        }
    }
}
