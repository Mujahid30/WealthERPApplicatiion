//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data;
//using Telerik.Web.UI;
//using System.Web.UI.HtmlControls;
//using BoAdvisorProfiling;
//using BoCommon;
//using VoUser;
//using Microsoft.ApplicationBlocks.ExceptionManagement;
//using System.Collections.Specialized;


//namespace WealthERP.Advisor
//{
//    public partial class ProductgroupSetup : System.Web.UI.UserControl
//    {
//        ProductgroupSetupBo  PSBo;
//        AdvisorVo advisorVo = new AdvisorVo();
//        UserVo UserVo = new UserVo();
//        DataSet dsHierarchyClusterDetails;
//        DataSet dsRMAndHierarchyDetails;
//        DataTable dtTeam = new DataTable();
//        DataTable dtChanel = new DataTable();
//        DataTable dtReportingTo = new DataTable();
//        DataTable dtHirarchy= new DataTable();
//        int SeqNO;
//        int adviserId;
//        int type;

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            SessionBo.CheckSession();
//            //fill the advisorVo from the session
//            advisorVo = (AdvisorVo)Session["advisorVo"];
//            //fill the userVO from the session 
//            UserVo = (UserVo)Session["userVo"];
//            if (!IsPostBack)
//            {
//                BindHierarchyClusterGrid(advisorVo.advisorId);

//            }
//        }

//        protected void rcbChanel_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
//        {
//            try
//            {
//                int SeqNNo;
//                PSBo = new ProductgroupSetupBo();
//                RadComboBox RadComboBox = (RadComboBox)sender;
//                GridEditableItem editedItem = RadComboBox.NamingContainer as GridEditableItem;
//                RadComboBox rcbChanel = editedItem.FindControl("rcbChanel") as RadComboBox;
//                //RadComboBox rcbTeamm = editedItem.FindControl("rcbTeamm") as RadComboBox;
//                 RadComboBox rcbReportingTo = editedItem.FindControl("rcbReportingTo") as RadComboBox;
//                //RadComboBox rcbTitles = editedItem.FindControl("rcbTitles") as RadComboBox;
//                 TextBox TxtSeq = (TextBox)editedItem.FindControl("TxtSeq");


//                 //SeqNNo = PSBo.GetTopSeqNo(advisorVo.advisorId);

//                 //if (SeqNNo != 0 & SeqNNo != 1)
//                 //{
//                     SeqNNo = PSBo.GetSeqNoinChanel(Convert.ToInt32(rcbChanel.SelectedValue), advisorVo.advisorId);
//                 //}
//                 TxtSeq.Text = SeqNNo.ToString();    
//                //dtTeam = PSBo.GetTeam().Tables[0];                
//                //rcbTeamm.DataSource = dtTeam;
//                //rcbTeamm.DataTextField = "WHLM_Name";
//                //rcbTeamm.DataValueField = "WHLM_Id";
//                //rcbTeamm.DataBind();

//                //dtChanel = PSBo.GetChanel().Tables[0];
//                //rcbChanel.DataSource = dtChanel;
//                //rcbChanel.DataTextField = "WHLM_Name";
//                //rcbChanel.DataValueField = "WHLM_Id";
//                //rcbChanel.DataBind();

//                 dtReportingTo = PSBo.GetReportsTo(Convert.ToInt32(rcbChanel.SelectedValue), advisorVo.advisorId).Tables[0];
//                rcbReportingTo.DataSource = dtReportingTo;
//                rcbReportingTo.DataTextField = "AH_HierarchyName";
//                rcbReportingTo.DataValueField  = "AH_TitleId";
//                //rcbReportingTo.DataTextField = "WHLM_Name";
//                //rcbReportingTo.DataValueField = "WHLM_Id";
//                rcbReportingTo.DataBind();

//                //dtHirarchy  = PSBo.GetHirarchy().Tables[0];
//                //rcbTitles.DataSource = dtHirarchy;
//                //rcbTitles.DataTextField = "WHLM_Name";
//                //rcbTitles.DataValueField = "WHLM_Id";
//                //rcbTitles.DataBind();



//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserHierarchyCluster.ascx:rcbSetup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)");
//                object[] objects = new object[3];
//                objects[0] = sender;
//                objects[1] = e;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }

//        }
//        protected void rcbSetup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
//        {
//            try
//            {
//                int SeqNNo;
//                PSBo = new ProductgroupSetupBo();
//                RadComboBox RadComboBox = (RadComboBox)sender;
//                GridEditableItem editedItem = RadComboBox.NamingContainer as GridEditableItem;
//                RadComboBox rcbChanel = editedItem.FindControl("rcbChanel") as RadComboBox;
//                RadComboBox rcbTeamm = editedItem.FindControl("rcbTeamm") as RadComboBox;
//                RadComboBox rcbReportingTo = editedItem.FindControl("rcbReportingTo") as RadComboBox;
//                RadComboBox rcbTitles = editedItem.FindControl("rcbTitles") as RadComboBox;
//                TextBox TxtSeq = (TextBox)editedItem.FindControl("TxtSeq");

//                Label Lb1Report = (Label)editedItem.FindControl("Label4");

//                dtTeam = PSBo.GetTeam().Tables[0];                
//                rcbTeamm.DataSource = dtTeam;
//                rcbTeamm.DataTextField = "WHLM_Name";
//                rcbTeamm.DataValueField = "WHLM_Id";
//                rcbTeamm.DataBind();

//                dtChanel = PSBo.GetChanel().Tables[0];
//                rcbChanel.DataSource = dtChanel;
//                rcbChanel.DataTextField = "WHLM_Name";
//                rcbChanel.DataValueField = "WHLM_Id";
//                rcbChanel.DataBind();

//                //Getting Seq NO 

               
//                rcbChanel.SelectedValue = "0";


//                //SeqNNo = PSBo.GetTopSeqNo(advisorVo.advisorId);

//                //if (SeqNNo != 0 & SeqNNo != 1)
//                //{
//                    SeqNNo = PSBo.GetSeqNoinChanel(Convert.ToInt32(rcbChanel.SelectedValue), advisorVo.advisorId);
//                //}
//               // SeqNNo = PSBo.GetSeqNoinChanel(Convert.ToInt32(rcbChanel.SelectedValue));

//                if (SeqNNo == 0 | SeqNNo == 1)
//                {
//                    rcbReportingTo.Visible = false;
//                    Lb1Report.Visible = false;
//                   // Lb1Title.Visible = false;
//                }
//                else
//                {
//                    rcbReportingTo.Visible = true;
//                    Lb1Report.Visible = true;
//                  //  Lb1Title.Visible = true;
//                }

//                TxtSeq.Text = SeqNNo.ToString();
//                dtReportingTo = PSBo.GetReportsTo(Convert.ToInt32(rcbChanel.SelectedValue), advisorVo.advisorId).Tables[0];
//                rcbReportingTo.DataSource = dtReportingTo;
//                rcbReportingTo.DataTextField = "AH_HierarchyName";
//                rcbReportingTo.DataValueField  = "AH_TitleId";
//                rcbReportingTo.DataBind();

//                //dtHirarchy  = PSBo.GetHirarchy().Tables[0];
//                //rcbTitles.DataSource = dtHirarchy;
//                //rcbTitles.DataTextField = "WHLM_Name";
//                //rcbTitles.DataValueField = "WHLM_Id";
//                //rcbTitles.DataBind();



//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserHierarchyCluster.ascx:rcbSetup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)");
//                object[] objects = new object[3];
//                objects[0] = sender;
//                objects[1] = e;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }

//        }
//        public void BindHierarchyClusterGrid(int adviserId)
//        {
//            try
//            {
//                dsHierarchyClusterDetails = new DataSet();
//                PSBo = new ProductgroupSetupBo();
//                //getting dataset for grid and the rm ddl and the Hierarchy name ddl
//                dsHierarchyClusterDetails = PSBo.GetHierarchyDetails(advisorVo.advisorId);//, Convert.ToInt32(rcbShow.SelectedValue));
//                gvHirarchy.DataSource = dsHierarchyClusterDetails;
//                gvHirarchy.DataBind();
//                //set the visibility for export button
              
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserHierarchyCluster.ascx:BindHierarchyClusterGrid(int adviserId)");
//                object[] objects = new object[3];
//                objects[0] = adviserId;
//                objects[1] = type;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }


//        private bool ChkSeq(int Chanelid,String Seqno )
//        {
//            bool result=false;
//            DataTable dt = new DataTable();
//            PSBo = new ProductgroupSetupBo();
//            dt = PSBo.GetSeq(Chanelid, advisorVo.advisorId).Tables[0];

//            if (dt != null)
//            {
//                for (int i = 0; i < dt.Rows.Count; i++)
//                {

//                    if (Seqno == dt.Rows[i]["AH_Sequence"].ToString() )
//                    {
//                        result=true;
//                        return result;
//                    }
//                }

                    
//            }


//            return result;
//        }
//        private bool ChkHirarchy(String Titlename,int Chanelid)
//        {
//            bool result = false;
//            DataTable dt = new DataTable();
//            PSBo = new ProductgroupSetupBo();
//            dt = PSBo.GetHirarchy(Chanelid, advisorVo.advisorId).Tables[0];

//            if (dt != null)
//            {
//                for (int i = 0; i < dt.Rows.Count; i++)
//                {

//                    if (Titlename.ToUpper() == dt.Rows[i]["AH_HierarchyName"].ToString().ToUpper())
//                    {
//                        result = true;
//                        return result;
//                    }
//                }


//            }


//            return result;
//        }
//        protected void gvHirarchy_ItemCommand(object source, GridCommandEventArgs e)
//        {
//            string description = string.Empty;
//            string name = string.Empty;
//            string insertType = string.Empty;

//            if (e.CommandName == RadGrid.UpdateCommandName)
//            {
//                int AZOC_HierarchyId = 0;
//                PSBo = new ProductgroupSetupBo();
//                bool isUpdated = false;
//                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;


//                RadComboBox rcbSetup = (RadComboBox)e.Item.FindControl("rcbSetup");
//                RadComboBox rcbChanel = (RadComboBox)e.Item.FindControl("rcbChanel");
//                TextBox txtHierarchy = (TextBox)e.Item.FindControl("txtHierarchy");
//                RadComboBox rcbTeamm = (RadComboBox)e.Item.FindControl("rcbTeamm");
//                RadComboBox rcbReportingTo = (RadComboBox)e.Item.FindControl("rcbReportingTo");
//                RadComboBox Radops = (RadComboBox)e.Item.FindControl("Radops");
//                RadComboBox rcbTitles = e.Item.FindControl("rcbTitles") as RadComboBox;
//                TextBox TxtSeq = (TextBox)e.Item.FindControl("TxtSeq");

//                int Hid =Convert.ToInt32 ( gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_Id"].ToString());
//                int SeqsNo = Convert.ToInt32(gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_Sequence"].ToString());

//                if (SeqsNo == Convert.ToInt32(TxtSeq.Text))
//                {
//                    isUpdated = PSBo.HierarchyDetailsDetailsAddEditDelete(advisorVo.advisorId, Hid, txtHierarchy.Text, Convert.ToInt32(rcbTitles.SelectedValue), rcbTeamm.Text, Convert.ToInt32(rcbTeamm.SelectedValue), Convert.ToInt32(rcbReportingTo.SelectedValue), rcbReportingTo.Text, rcbChanel.Text, Convert.ToInt32(rcbChanel.SelectedValue), Convert.ToInt32(TxtSeq.Text), rcbSetup.Text, e.CommandName);
//                }
//                else
//                {
//                    if (ChkSeq(Convert.ToInt32(rcbChanel.SelectedValue), TxtSeq.Text) == true)
//                    {
//                        Response.Write(@"<script language='javascript'>alert('This Sequence alerady entered');</script>");
//                        return;
//                    }
//                    else
//                    {
//                        isUpdated = PSBo.HierarchyDetailsDetailsAddEditDelete(advisorVo.advisorId, Hid, txtHierarchy.Text, Convert.ToInt32(rcbTitles.SelectedValue), rcbTeamm.Text, Convert.ToInt32(rcbTeamm.SelectedValue), Convert.ToInt32(rcbReportingTo.SelectedValue), rcbReportingTo.Text, rcbChanel.Text, Convert.ToInt32(rcbChanel.SelectedValue), Convert.ToInt32(TxtSeq.Text), rcbSetup.Text, e.CommandName);

//                    }
//                }                
                


//               // if (AZOC_Type != "Hierarchy")
//               // {
//               //     AZOC_HierarchyId = Convert.ToInt32(rcbPickAHierarchy.SelectedValue);
//               // }

//               // string HierarchyName = txtName.Text;
//               // int AZOC_HierarchyClusterId = Convert.ToInt32(gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_HierarchyClusterId"].ToString());
//               // type = Convert.ToInt32(rcbEditFormAddType.SelectedValue);
//               // if (type == 1)
//               //     insertType = "Hierarchy";
//               // else
//               //     insertType = "Cluster";
//               // //check if update then show the message
//               //// isUpdated = PSBo.HierarchyClusterDetailsAddEditDelete(advisorVo.advisorId, Convert.ToInt32(rcbHead.SelectedValue), AZOC_HierarchyClusterId, AZOC_HierarchyId, txtDescription.Text, txtName.Text, insertType, 0, UserVo.UserId, DateTime.MinValue, DateTime.Now, e.CommandName);
//                if (isUpdated == false)
//                    Response.Write(@"<script language='javascript'>alert('The error updating Hierarchy : \n" + "" + "');</script>");
//                else
//                    Response.Write(@"<script language='javascript'>alert('The Hierarchy: \n" + "" + " updated successfully.');</script>");
//            }
//            if (e.CommandName == RadGrid.PerformInsertCommandName)
//            {
//                PSBo = new ProductgroupSetupBo();
//                bool isInserted = false;
//                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
//                RadComboBox rcbSetup = (RadComboBox)e.Item.FindControl("rcbSetup");
//                RadComboBox rcbChanel = (RadComboBox)e.Item.FindControl("rcbChanel");
//                TextBox txtHierarchy = (TextBox)e.Item.FindControl("txtHierarchy");
//                RadComboBox rcbTeamm = (RadComboBox)e.Item.FindControl("rcbTeamm");
//                RadComboBox rcbReportingTo = (RadComboBox)e.Item.FindControl("rcbReportingTo");
//                RadComboBox Radops = (RadComboBox)e.Item.FindControl("Radops");
//                RadComboBox rcbTitles = e.Item.FindControl("rcbTitles") as RadComboBox;
//                TextBox TxtSeq = (TextBox)e.Item.FindControl("TxtSeq");

//                 int Rid;
//                    int RTitId;
//                    int RChanelid;
//                    int RTeamid;

                    
//                    if (rcbReportingTo.Items.Count>0)
//                    {
//                    Rid =    Convert.ToInt32(rcbReportingTo.SelectedValue);
//                    }
//                    else
//                    {
//                    Rid=0;
//                    }
                    
//                    if (rcbTeamm.SelectedValue == "")
//                    {
//                        RTeamid = 0;
//                    }
//                    else
//                    {
//                        RTeamid = Convert.ToInt32(rcbTeamm.SelectedValue);
//                    }
                     
//                        if (rcbChanel.SelectedValue == "")
//                    {
//                        RChanelid = 0;
//                    }
//                    else
//                    {
//                        RChanelid = Convert.ToInt32(rcbChanel.SelectedValue);
//                    }
//                        if (ChkSeq(RChanelid, TxtSeq.Text) == true)
//                {
//                    Response.Write(@"<script language='javascript'>alert('Sequence alerady exist');</script>");
//                    return;
//                }
//                else if (ChkHirarchy(txtHierarchy.Text, RChanelid))
//                {
//                    Response.Write(@"<script language='javascript'>alert('Title name  alerady for this chanel exist');</script>");
//                    return;
//                }
//                else
//                {
                   
//                  //  Convert.ToInt32(rcbTitles.SelectedValue)

//                        isInserted = PSBo.HierarchyDetailsDetailsAddEditDelete(advisorVo.advisorId, 0, txtHierarchy.Text.ToUpper(), 0, rcbTeamm.Text,RTeamid , Rid, rcbReportingTo.Text, rcbChanel.Text, RChanelid, Convert.ToInt32(TxtSeq.Text), rcbSetup.Text, e.CommandName);
//                    if (isInserted == false)
//                        Response.Write(@"<script language='javascript'>alert('Error inserting records');</script>");
//                    else
//                        Response.Write(@"<script language='javascript'>alert('Records inserted successfully');</script>");
//                }
//            }
//            if (e.CommandName == RadGrid.DeleteCommandName)
//            {
//                bool isDeleted = false;
//                PSBo = new ProductgroupSetupBo();
//                GridDataItem dataItem = (GridDataItem)e.Item;
//                TableCell HirarchyIdForDelete = dataItem["AH_Id"];
//                //string HierarchyName = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_Name"].ToString();
//                int HId = int.Parse(HirarchyIdForDelete.Text);
//                //string deleteType = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_Type"].ToString();
//                //check if deleted then show message
//                isDeleted = PSBo.HierarchyDetailsDetailsAddEditDelete(advisorVo.advisorId,  HId,"",0,string.Empty, 0,0, string.Empty, "",0,0,"", e.CommandName);
//                if (isDeleted == false )
//                    Response.Write(@"<script language='javascript'>alert('The Hierarchy : \n" + "" + " Cannot be deleted since it is attached to a Hierarchy.');</script>");
//                else
//                    Response.Write(@"<script language='javascript'>alert('The Hierarchy: \n" + "" + " deleted successfully.');</script>");
//            }
//            //bind the grid to get the edit form mode
//           BindHierarchyClusterGrid(advisorVo.advisorId );
//        }
//        protected void gvHirarchy_ItemDataBound(object sender, GridItemEventArgs e)
//        {
//            dsRMAndHierarchyDetails = new DataSet();
//            PSBo = new ProductgroupSetupBo();

//            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
//            {
//                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
//                DataTable dtAccType = new DataTable();
//                DataTable dtTeam = new DataTable();
//                DataTable dtChanel = new DataTable();

//                GridEditFormItem gefi = (GridEditFormItem)e.Item;
//                RadComboBox rcbChanel = (RadComboBox)gefi.FindControl("rcbChanel");
//                RadComboBox rcbTeamm = (RadComboBox)gefi.FindControl("rcbTeamm");

//                //rcbSetup_SelectedIndexChanged(null , null);
//                RadComboBoxItem defaultItem = new RadComboBoxItem();
//                //setting default for radcombobox
//                defaultItem.Text = "";
//                defaultItem.Value = "";

              
//            }

//            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
//            {
//                //finding data key names for type , name and Hierarchy to fill the ddl respectively
//                string Hid = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_Id"].ToString();
               
//                GridEditFormItem editedItem = (GridEditFormItem)e.Item;

//                //finding control for ddl type , head and Hierarchy


//                RadComboBox rcbSetup = (RadComboBox)e.Item.FindControl("rcbSetup");
//                RadComboBox rcbChanel = (RadComboBox)e.Item.FindControl("rcbChanel");
//                TextBox txtHierarchy = (TextBox)e.Item.FindControl("txtHierarchy");
//                RadComboBox rcbTeamm = (RadComboBox)e.Item.FindControl("rcbTeamm");
//                RadComboBox rcbReportingTo = (RadComboBox)e.Item.FindControl("rcbReportingTo");
//                RadComboBox Radops = (RadComboBox)e.Item.FindControl("Radops");
//                RadComboBox rcbTitles = e.Item.FindControl("rcbTitles") as RadComboBox;
//                TextBox TxtSeq = (TextBox)e.Item.FindControl("TxtSeq");

//                string TitleId = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_TitleId"].ToString();
//                string TeamId = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_TeamId"].ToString();
//                string ReportsToId = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_ReportsToId"].ToString();
//                string ChannelId = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_ChannelId"].ToString();
//                string  RoleName = gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_HierarchyName"].ToString();

//                SeqNO = Convert.ToInt32(gvHirarchy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AH_Sequence"]);

               

//                DataTable dtReportsTo = new DataTable();

                 
//               //AH_HierarchyName,AH_TitleId,AH_Teamname,AH_TeamId,AH_ReportsToId,AH_ReportsTo,AH_ChannelName
               

//                dtTeam = PSBo.GetTeam().Tables[0];
//                dtChanel = PSBo.GetChanel().Tables[0];

//                if (rcbSetup.Text == "Team" | rcbSetup.Text == "team")
//                {
//                    rcbSetup.SelectedValue = "0";
//                }
//                else
//                {
//                    rcbSetup.SelectedValue = "1";
//                }


//                //filling the data for Hierarchy ddl
//                rcbTeamm.DataSource = dtTeam;
//                rcbTeamm.DataTextField = "WHLM_Name";
//                rcbTeamm.DataValueField = "WHLM_Id";
//                rcbTeamm.DataBind();

//                //filling the data from rm ddl
//                rcbChanel.DataSource = dtChanel;
//                rcbChanel.DataTextField = "WHLM_Name";
//                rcbChanel.DataValueField = "WHLM_Id";
//                rcbChanel.DataBind();


//                dtReportingTo = PSBo.GetReportsTo(Convert.ToInt32(rcbChanel.SelectedValue), advisorVo.advisorId).Tables[0];
//                rcbReportingTo.DataSource = dtReportingTo;
//                rcbReportingTo.DataTextField = "AH_HierarchyName";
//                rcbReportingTo.DataValueField  = "AH_TitleId";
//                //rcbReportingTo.DataTextField = "WHLM_Name";
//                //rcbReportingTo.DataValueField = "WHLM_Id";
//                rcbReportingTo.DataBind();

//                dtHirarchy = PSBo.GetHirarchy(Convert.ToInt32(rcbChanel.SelectedValue), advisorVo.advisorId).Tables[0];
//                rcbTitles.DataSource = dtHirarchy;
//                rcbTitles.DataTextField = "WHLM_Name";
//                rcbTitles.DataValueField = "WHLM_Id";
//                rcbTitles.DataBind();


//                TxtSeq.Text = SeqNO.ToString();
//                txtHierarchy.Text = RoleName;
//                rcbReportingTo.SelectedValue = ReportsToId;
//                rcbTitles.SelectedValue = TitleId;
//                rcbChanel.SelectedValue = ChannelId;
//                rcbTeamm.SelectedValue = TeamId;

                 
//            }
//        }

//        protected void rcbEditFormAddType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
//        {
//            try
//            {
//               RadComboBox RadComboBox = (RadComboBox)sender;
//               GridEditableItem editedItem = RadComboBox.NamingContainer as GridEditableItem;
//                //RadComboBox rcbEditFormAddType = editedItem.FindControl("rcbEditFormAddType") as RadComboBox;
//                //HtmlTableRow trPickAHierarchy = editedItem.FindControl("trPickAHierarchy") as HtmlTableRow;
//               RadComboBox rcbChanel = editedItem.FindControl("rcbChanel") as RadComboBox;
//               RadComboBox rcbTeamm = editedItem.FindControl("rcbTeamm") as RadComboBox;
//                //RadComboBox rcbChanel = (RadComboBox)e.Item.FindControl("rcbChanel");
//                //RadComboBox rcbTeamm = (RadComboBox)e.Item.FindControl("rcbTeamm");
//                ////if (rcbEditFormAddType.SelectedValue == "2")
//                //    trPickAHierarchy.Visible = true;
//                //else
//                //    trPickAHierarchy.Visible = false;

//                //bind RM on the basis of zonal head or the cluster head
//                DataTable dtAccType = new DataTable();
//                DataTable dtHierarchyDetail = new DataTable();
//                DataTable dtRMDetail = new DataTable();

//                //getting the RMDetail from cache                
//               // dtRMDetail = (DataTable)Cache["RMDetail" + advisorVo.advisorId];

//                //getting the HierarchyDetail from cache 
//               // dtHierarchyDetail = (DataTable)Cache["HierarchyDetail" + advisorVo.advisorId];

//                RadComboBoxItem defaultItem = new RadComboBoxItem();
//                //setting default for radcombobox // need to remove after adding the head 
//                defaultItem.Text = "0";
//                defaultItem.Value = "0";


//                RadComboBoxItem defaultItemPickAHierarchy = new RadComboBoxItem();
//                //setting default for radcombobox
//                defaultItemPickAHierarchy.Text = "";
//                defaultItemPickAHierarchy.Value = "";

//                //declare a table to store the filtered data
//                DataTable dtZonalClusterHeadFilter = new DataTable();

//                //filtering the datasource on the basis of Hierarchy and cluster
//                //if (rcbEditFormAddType.SelectedValue == "1")
//                //{
//                //    string JobFunctionFilter;
//                //    string typeFilter;
//                //    JobFunctionFilter = "AR_JobFunction=" + "'Zonal Head'";
//                //    typeFilter = "AZOC_Type=" + "'Hierarchy'";
//                //    //dataview creation for job function
//                //    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, JobFunctionFilter, "AR_JobFunction", DataViewRowState.CurrentRows);
//                //    dtRMDetail = dvMFTransactionsProcessed.ToTable();
//                //    //dataview creation for type filter
//                //    DataView dvTypeFilter = new DataView(dtHierarchyDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
//                //    dtHierarchyDetail = dvTypeFilter.ToTable();
//                //}
//                //else
//                //{
//                //    string expressionForRowsWithoutFM;
//                //    string typeFilter;
//                //    expressionForRowsWithoutFM = "AR_JobFunction=" + "'Cluster Head'";
//                //    typeFilter = "AZOC_Type=" + "'Hierarchy'";
//                //    //dataview creation for job function
//                //    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, expressionForRowsWithoutFM, "AR_JobFunction", DataViewRowState.CurrentRows);
//                //    dtRMDetail = dvMFTransactionsProcessed.ToTable();
//                //    //dataview creation for type filter
//                //    DataView dvTypeFilter = new DataView(dtHierarchyDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
//                //    dtHierarchyDetail = dvTypeFilter.ToTable();
//                //}

//                //binding the ddl RM
//                //RadComboBox rcbChanel = (RadComboBox)e.Item.FindControl("rcbChanel");
//                //RadComboBox rcbTeamm = (RadComboBox)e.Item.FindControl("rcbTeamm");
//                dtTeam = PSBo.GetTeam().Tables[0];
//                dtChanel = PSBo.GetChanel().Tables[0];


//                //filling the data for Hierarchy ddl
//                rcbTeamm.DataSource = dtTeam;
//                rcbTeamm.DataTextField = "WHLM_Name";
//                rcbTeamm.DataValueField = "WHLM_Id";
//                rcbTeamm.DataBind();

//                //filling the data from rm ddl
//                rcbChanel.DataSource = dtChanel;
//                rcbChanel.DataTextField = "WHLM_Name";
//                rcbChanel.DataValueField = "WHLM_Id";
//                rcbChanel.DataBind();

//                //rcbHead.DataSource = dtRMDetail;
//                //rcbHead.DataTextField = "name";
//                //rcbHead.DataValueField = "ar_rmid";
//                //rcbHead.DataBind();

//                ////binding ddl Hierarchy
//                //rcbPickAHierarchy.DataSource = dtHierarchyDetail;
//                //rcbPickAHierarchy.DataTextField = "AZOC_Name";
//                //rcbPickAHierarchy.DataValueField = "AZOC_HierarchyClusterId";
//                //rcbPickAHierarchy.DataBind();

//                //rcbHead.Items.Insert(0, defaultItem);
//                //rcbPickAHierarchy.Items.Insert(0, defaultItemPickAHierarchy);
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserHierarchyCluster.ascx:rcbEditFormAddType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)");
//                object[] objects = new object[3];
//                objects[0] = sender;
//                objects[1] = e;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }






//        protected void gvHirarchy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
//        {
//            DataTable dtGvSchemeDetails = new DataTable();
//            try
//            {
//                dsHierarchyClusterDetails = new DataSet();
//                PSBo = new ProductgroupSetupBo();
//                //getting dataset for grid and the rm ddl and the Hierarchy name ddl
//                dsHierarchyClusterDetails = PSBo.GetHierarchyDetails(advisorVo.advisorId);//, Convert.ToInt32(rcbShow.SelectedValue));
//                gvHirarchy.DataSource = dsHierarchyClusterDetails;
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "ProductgroupSetup.ascx:gvHirarchy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)");
//                object[] objects = new object[3];
//                objects[0] = dtGvSchemeDetails;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }
//    }
//}