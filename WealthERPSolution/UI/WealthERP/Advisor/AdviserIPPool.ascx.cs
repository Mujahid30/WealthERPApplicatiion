using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using VoUser;
using WealthERP.Base;
using BoAdvisorProfiling;

namespace WealthERP.Advisor
{
    public partial class AdviserIPPool : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        UserVo tempuservo;
        DataTable dt = new DataTable();
        AdvisorVo advisorVo = new AdvisorVo();
        DataRow dr;
        string Role = "";
        AdviserIPVo adviserIPVo = new AdviserIPVo();
        AdvisorBo advisorBo = new AdvisorBo();
        DataSet dsGetAllAdviserIPFromIPPool = new DataSet();
        DataSet getAllLoggedinIPs = new DataSet();
        int totalAdviserEnteredIPCount = 0;
        bool RecordStatus = false;

        //For Edit 
        int totalRecordsCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                int adviserId = 0;
                advisorVo = (AdvisorVo)Session["advisorVo"];
                adviserId = advisorVo.advisorId;

                if (!IsPostBack)
                {
                    dt = new DataTable();
                    dt.Columns.Add("A_AdviserId");
                    dt.Columns.Add("AIPP_poolId");
                    dt.Columns.Add("AIPP_IP");
                    dt.Columns.Add("AIPP_Comments");
                    Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                }
                else
                {
                    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                }
                getAllLoggedinIPs = advisorBo.GetAdvisersAlreadyLoggedIPs(advisorVo.advisorId);
                if (!IsPostBack)
                {
                    dsGetAllAdviserIPFromIPPool = advisorBo.GetAdviserIPPoolsInformation(advisorVo.advisorId);
                    if (dsGetAllAdviserIPFromIPPool.Tables.Count != 0)
                        totalAdviserEnteredIPCount = dsGetAllAdviserIPFromIPPool.Tables[0].Rows.Count;



                    if (getAllLoggedinIPs.Tables.Count != 0)
                        totalRecordsCount = getAllLoggedinIPs.Tables[0].Rows.Count;

                    if (totalAdviserEnteredIPCount != 0)
                    {
                        btnSubmit.Text = "Update";
                    }
                    else
                    {
                        btnSubmit.Text = "Submit";
                    }

                    // Getting already Entered IP addresses from the database and binding it to Grid..
                    if (dsGetAllAdviserIPFromIPPool.Tables.Count != 0)
                    {
                        if (dsGetAllAdviserIPFromIPPool.Tables[0].Rows.Count != 0)
                        {
                            dt.Rows.Clear();
                            foreach (DataRow drAdviserIPPool in dsGetAllAdviserIPFromIPPool.Tables[0].Rows)
                            {
                                DataRow dr = dt.NewRow();

                                if ((drAdviserIPPool["A_AdviserId"] != null) || (drAdviserIPPool["A_AdviserId"].ToString() != ""))
                                    dr["A_AdviserId"] = drAdviserIPPool["A_AdviserId"];

                                if ((drAdviserIPPool["AIPP_poolId"] != null) || (drAdviserIPPool["AIPP_poolId"].ToString() != ""))
                                    dr["AIPP_poolId"] = drAdviserIPPool["AIPP_poolId"];

                                if ((drAdviserIPPool["AIPP_IP"] != null) || (drAdviserIPPool["AIPP_IP"].ToString() != ""))
                                    dr["AIPP_IP"] = drAdviserIPPool["AIPP_IP"];

                                if ((drAdviserIPPool["AIPP_Comments"] != null) || (drAdviserIPPool["AIPP_Comments"].ToString() != ""))
                                    dr["AIPP_Comments"] = drAdviserIPPool["AIPP_Comments"];

                                dt.Rows.Add(dr);
                            }
                        }
                    }

                    // Session[SessionContents.FPS_AddProspect_DataTable] is a Session Used to store the datatable Which contains data to bind the IP Grid. 
                    Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                }

                // Session[SessionContents.FPS_AddProspectListActionStatus] is a Session which is used for storing the current Mode(View/Edit) of the page.
                if (Session[SessionContents.FPS_AddProspectListActionStatus] == "View")
                {
                    RadGrid1.Enabled = false;
                    btnSubmit.Visible = false;
                    btnGetIPsfromlog.Visible = false;
                    AdviserIPPoolPanel.Enabled = false;
                    RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;
                }
                else if (Session[SessionContents.FPS_AddProspectListActionStatus] == "Edit")
                {
                    RadGrid1.Enabled = true;
                    btnSubmit.Visible = true;
                    btnGetIPsfromlog.Visible = true;
                    AdviserIPPoolPanel.Enabled = true;
                    RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
                try
                {
                    if (dt.Rows[e.Item.ItemIndex]["AIPP_poolId"].ToString() != "")
                    {
                        Session["AdviserIPPools"] = dt.Rows[e.Item.ItemIndex]["AIPP_poolId"].ToString();
                        int rowCount = dt.Rows.Count;

                        if (rowCount != 1)
                        {
                            dt.Rows[e.Item.ItemIndex].Delete();
                            DeleteAdviserIPFromGridView();
                        }
                        else
                        {
                            ChildDeletionFunction();
                        }
                    }
                    else
                    {
                        dt.Rows[e.Item.ItemIndex].Delete();
                    }
                }
                catch (Exception ex)
                {
                    RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to delete</strong>"));
                    e.Canceled = true;

                }
                Rebind();
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;
            }

        }

        private void ChildDeletionFunction()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:showmessage();", true);
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                int i = 2;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
                foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                {

                    if (column is IGridEditableColumn)
                    {
                        IGridEditableColumn editableCol = (column as IGridEditableColumn);
                        if (editableCol.IsEditable)
                        {
                            IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                            string editorType = editor.ToString();
                            string editorText = "unknown";
                            object editorValue = null;

                            if (editor is GridTextColumnEditor)
                            {
                                editorText = (editor as GridTextColumnEditor).Text;
                                editorValue = (editor as GridTextColumnEditor).Text;
                            }
                            if (editor is GridBoolColumnEditor)
                            {
                                editorText = (editor as GridBoolColumnEditor).Value.ToString();
                                editorValue = (editor as GridBoolColumnEditor).Value;
                            }
                            if (editor is GridDropDownColumnEditor)
                            {
                                editorText = (editor as GridDropDownColumnEditor).SelectedValue;
                                editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                            }
                            if (editor is GridTemplateColumnEditor)
                            {
                                if (i != 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtIPName");
                                    editorText = txt.Text;
                                    editorValue = txt.Text;
                                }
                                else if (i == 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtIPComments");
                                    editorText = txt.Text;
                                    editorValue = txt.Text;
                                }

                            }
                            try
                            {
                                DataRow[] changedrows = dt.Select();
                                changedrows[editedItem.ItemIndex][column.UniqueName] = editorValue;


                            }
                            catch (Exception ex)
                            {
                                RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to set value of column '" + column.UniqueName + "'</strong> - " + ex.Message));
                                e.Canceled = true;
                                break;
                            }
                        }
                        i++;
                    }

                }
                Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                Rebind();
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;
            }
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                int i = 2;
                int j = 0;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
                foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                {
                    if (column is IGridEditableColumn)
                    {
                        IGridEditableColumn editableCol = (column as IGridEditableColumn);
                        if (editableCol.IsEditable)
                        {
                            IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                            string editorType = editor.ToString();
                            string editorText = "unknown";
                            object editorValue = null;

                            if (editor is GridTextColumnEditor)
                            {
                                editorText = (editor as GridTextColumnEditor).Text;
                                editorValue = (editor as GridTextColumnEditor).Text;
                            }
                            if (editor is GridBoolColumnEditor)
                            {
                                editorText = (editor as GridBoolColumnEditor).Value.ToString();
                                editorValue = (editor as GridBoolColumnEditor).Value;
                            }
                            if (editor is GridDropDownColumnEditor)
                            {
                                editorText = (editor as GridDropDownColumnEditor).SelectedValue;
                                editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                            }
                            if (editor is GridTemplateColumnEditor)
                            {
                                if (i != 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtIPName");
                                    editorText = txt.Text;
                                    editorValue = txt.Text;
                                }
                                else if (i == 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtIPComments");
                                    editorText = txt.Text;
                                    editorValue = txt.Text;
                                }

                            }
                            try
                            {
                                dr[i] = editorText;

                            }
                            catch (Exception ex)
                            {
                                RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to set value of column '" + column.UniqueName + "'</strong> - " + ex.Message));
                                e.Canceled = true;
                                break;
                            }
                        }
                        i++;
                    }
                }
                dt.Rows.Add(dr);
                Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                Rebind();
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            Rebind();
        }

        /// <summary>
        /// Used to bind Data to RadGrid
        /// </summary>
        protected void Rebind()
        {
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            RadGrid1.DataSource = dt;
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        protected void BindRelation(DropDownList ddList)
        {

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            tempuservo = (UserVo)Session["uservo"];
            int createdById = tempuservo.UserId;
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            if (dt.Rows.Count != 0)
            {
                if (btnSubmit.Text == "Submit")
                {
                    foreach (DataRow drIPsForAdviser in dt.Rows)
                    {
                        CreateAdviserIPPools(drIPsForAdviser, createdById);
                    }
                }
                else if (btnSubmit.Text == "Update")
                {
                    foreach (DataRow drIPsForAdviser in dt.Rows)
                    {
                        if (drIPsForAdviser["AIPP_poolId"] != null && drIPsForAdviser["AIPP_poolId"].ToString() != "")
                        {
                            UpdateAdviserIPPools(drIPsForAdviser, createdById, "Update");
                            if (RecordStatus == true)
                            {
                                msgRecordStatus.Visible = true;
                                msgRecordStatus.InnerText = "Record updated Successfully";
                            }
                            else
                                msgRecordStatus.Visible = false;
                        }
                        else
                        {
                            CreateAdviserIPPools(drIPsForAdviser, createdById);
                            if (RecordStatus == true)
                            {
                                msgRecordStatus.Visible = true;
                                msgRecordStatus.InnerText = "Record created Successfully";
                            }
                            else
                                msgRecordStatus.Visible = false;
                        }
                    }
                }

            }
        }

        private void CreateAdviserIPPools(DataRow drIPsForAdviser, int createdById)
        {
            adviserIPVo.advisorId = advisorVo.advisorId;
            adviserIPVo.AdviserIPs = drIPsForAdviser["AIPP_IP"].ToString();
            adviserIPVo.AdviserIPComments = drIPsForAdviser["AIPP_Comments"].ToString();
            RecordStatus = advisorBo.CreateAdviserIPPools(adviserIPVo, createdById);
            if (RecordStatus == true)
            {
                msgRecordStatus.Visible = true;
            }
            else
                msgRecordStatus.Visible = false;
        }

        private void UpdateAdviserIPPools(DataRow drIPsForAdviser, int createdById, string Flag)
        {
            adviserIPVo.advisorIPPoolId = int.Parse(drIPsForAdviser["AIPP_poolId"].ToString());
            adviserIPVo.AdviserIPs = drIPsForAdviser["AIPP_IP"].ToString();
            adviserIPVo.AdviserIPComments = drIPsForAdviser["AIPP_Comments"].ToString();
            RecordStatus = advisorBo.UpdateAdviserIPPools(adviserIPVo, createdById, Flag);


        }
        protected void aplToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Value == "View")
            {
                Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserStaffSMTP','login');", true);
            }
            else if (e.Item.Value == "Edit")
            {
                Session[SessionContents.FPS_AddProspectListActionStatus] = "Edit";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserStaffSMTP','login');", true);
            }
        }

        // To Delete the customers
        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            DeleteAdviserIPFromGridView();
        }

        protected void DeleteAdviserIPFromGridView()
        {
            int adviserIPId;

            adviserIPId = int.Parse(Session["AdviserIPPools"].ToString());
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {

                if (advisorBo.DeleteAdviserIPPool(adviserIPId, "Delete"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdviserStaffSMTP','login');", true);

                }
            }
            else
            {
                RecordStatus = advisorBo.DeleteAdviserIPPool(adviserIPId, "Delete");

                if (RecordStatus == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:DeleteAdviserIPs();", true);
                }

            }
        }

        protected void btnGetIPsfromlog_Click(object sender, EventArgs e)
        {

            if ((getAllLoggedinIPs.Tables.Count != 0) || (getAllLoggedinIPs.Tables[0].Rows.Count != 0))
            {
                mdlPopupGetIPlog.Show();
                mdlPopupGetIPlog.TargetControlID = "btnGetIPsfromlog";

                chklistIPPools.Visible = true;
                chklistIPPools.DataSource = getAllLoggedinIPs.Tables[0];
                chklistIPPools.DataTextField = getAllLoggedinIPs.Tables[0].Columns[0].ToString();
                chklistIPPools.DataValueField = getAllLoggedinIPs.Tables[0].Columns[0].ToString();
                chklistIPPools.DataBind();
                Session["CheckBoxIPCreation"] = "Create IPs For Advisor";

            }
            else
            {
                mdlPopupGetIPlog.Hide();
                chklistIPPools.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry you don't have any IPs to provide IP Security!');", true);
            }
        }

        protected void hiddenReloadPage_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdviserStaffSMTP','login');", true);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
        }

        protected void hdnGetIPbtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            string IPAddress = string.Empty;
            foreach (ListItem Li in chklistIPPools.Items)
            {
                if (Li.Selected)
                {
                    IPAddress = Li.Text;

                    DataRow dr = dt.NewRow();
                    dr["A_AdviserId"] = advisorVo.advisorId;
                    dr["AIPP_IP"] = IPAddress;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select one checkbox atleast..!!');", true);
            }
            Rebind();
            RadGrid1.MasterTableView.ClearEditItems();
            RadGrid1.MasterTableView.Rebind();

        }
    }
}