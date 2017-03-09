using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using BoAdvisorProfiling;
using BoCommon;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.Advisor
{
    public partial class AdviserZoneCluster : System.Web.UI.UserControl
    {
        AdvisorBo advisorBo;
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo UserVo = new UserVo();
        DataSet dsZoneClusterDetails;
        DataSet dsRMAndZoneDetails;
        int adviserId;
        int type;
        int zoneId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check for the session 
            SessionBo.CheckSession();
            //fill the advisorVo from the session
            advisorVo = (AdvisorVo)Session["advisorVo"];
            //fill the userVO from the session 
            UserVo = (UserVo)Session["userVo"];
           
            if (!IsPostBack)
            {
                //Clear the cahce when the page first loads                
                Cache.Remove("ZoneClusterDetails" + advisorVo.advisorId);
                Cache.Remove("RMDetail" + advisorVo.advisorId);
                Cache.Remove("ZoneDetail" + advisorVo.advisorId);
              
            }
        }


        /// <summary>
        /// This method will fire the event of the main combobox which will bind the grid in accordance to the selected type
        /// if it is zone or cluster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        protected void btnAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rcbShow.SelectedValue != null)
            {
                dsRMAndZoneDetails = new DataSet();
                try
                {
                    BindZoneClusterGrid(advisorVo.advisorId, Convert.ToInt32(rcbShow.SelectedValue));
                    gvZoneClusterDetails.Visible = true;
                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (Exception Ex)
                {
                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();
                    FunctionInfo.Add("Method", "AdviserZoneCluster.ascx:rcbShow_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)");
                    object[] objects = new object[3];
                    objects[0] = sender;
                    objects[1] = e;
                    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);
                    throw exBase;
                }
            }
            else
                gvZoneClusterDetails.Visible = false;
        }

        /// <summary>
        /// This is will fire the event of the combobox of the edit form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rcbEditFormAddType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                
                RadComboBox RadComboBox = (RadComboBox)sender;
                GridEditableItem editedItem = RadComboBox.NamingContainer as GridEditableItem;
                RadComboBox rcbEditFormAddType = editedItem.FindControl("rcbEditFormAddType") as RadComboBox;
                HtmlTableRow trPickAZone = editedItem.FindControl("trPickAZone") as HtmlTableRow;
                RadComboBox rcbHead = editedItem.FindControl("rcbHead") as RadComboBox;
                RadComboBox rcbPickAZone = editedItem.FindControl("rcbPickAZone") as RadComboBox;
                HtmlTableRow trPickArea = editedItem.FindControl("trPickArea") as HtmlTableRow;
                RadComboBox rcbPickArea = editedItem.FindControl("rcbPickArea") as RadComboBox;
                HtmlTableRow trPickAType = editedItem.FindControl("trPickAType") as HtmlTableRow;
                RadComboBox rcbEditForm = editedItem.FindControl("rcbEditForm") as RadComboBox;
                
                trPickAZone.Visible = false;
                trPickArea.Visible = false;
                trPickAType.Visible = false;
                if (rcbEditFormAddType.SelectedValue == "2")
                {
                    trPickArea.Visible = false;
                    trPickAType.Visible = true;
                    trPickAZone.Visible = true;
                }
                else if (rcbEditFormAddType.SelectedValue == "3")
                {
                    trPickAZone.Visible = true;
                    trPickArea.Visible = false;
                    trPickAType.Visible = false;
                }
                else
                {
                    trPickAZone.Visible = false;
                    trPickArea.Visible = false;
                    trPickAType.Visible = false;
                }
                //bind RM on the basis of zonal head or the cluster head
                DataTable dtAccType = new DataTable();
                DataTable dtZoneDetail = new DataTable();
                DataTable dtRMDetail = new DataTable();

                //getting the RMDetail from cache                
                dtRMDetail = (DataTable)Cache["RMDetail" + advisorVo.advisorId];

                //getting the ZoneDetail from cache 
                dtZoneDetail = (DataTable)Cache["ZoneDetail" + advisorVo.advisorId];

                RadComboBoxItem defaultItem = new RadComboBoxItem();
                //setting default for radcombobox // need to remove after adding the head 
                defaultItem.Text = "0";
                defaultItem.Value = "0";


                RadComboBoxItem defaultItemPickAZone = new RadComboBoxItem();
                //setting default for radcombobox
                defaultItemPickAZone.Text = "";
                defaultItemPickAZone.Value = "";

                //declare a table to store the filtered data
                DataTable dtZonalClusterHeadFilter = new DataTable();

                //filtering the datasource on the basis of zone and cluster
                if (rcbEditFormAddType.SelectedValue == "1")
                {
                    string JobFunctionFilter;
                    string typeFilter;
                    JobFunctionFilter = "AR_JobFunction=" + "'Zonal Head'";
                    typeFilter = "AZOC_Type=" + "'Zone'";
                    //dataview creation for job function
                    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, JobFunctionFilter, "AR_JobFunction", DataViewRowState.CurrentRows);
                    dtRMDetail = dvMFTransactionsProcessed.ToTable();
                    //dataview creation for type filter
                    DataView dvTypeFilter = new DataView(dtZoneDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
                    dtZoneDetail = dvTypeFilter.ToTable();
                }
                else if (rcbEditFormAddType.SelectedValue == "2")
                {
                    string expressionForRowsWithoutFM;
                    string typeFilter;
                    expressionForRowsWithoutFM = "AR_JobFunction=" + "'Cluster Head'";
                    typeFilter = "AZOC_Type=" + "'Zone'";
                    //dataview creation for job function
                    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, expressionForRowsWithoutFM, "AR_JobFunction", DataViewRowState.CurrentRows);
                    dtRMDetail = dvMFTransactionsProcessed.ToTable();
                    //dataview creation for type filter
                    DataView dvTypeFilter = new DataView(dtZoneDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
                    dtZoneDetail = dvTypeFilter.ToTable();
                }
                else
                {

                    string expressionForRowsWithout;
                    string typeFilter;
                    expressionForRowsWithout = "AR_JobFunction=" + "'Area Head'";
                    typeFilter = "AZOC_Type=" + "'Zone'";
                    //dataview creation for job function
                    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, expressionForRowsWithout, "AR_JobFunction", DataViewRowState.CurrentRows);
                    dtRMDetail = dvMFTransactionsProcessed.ToTable();
                    //dataview creation for type filter
                    DataView dvTypeFilter = new DataView(dtZoneDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
                    dtZoneDetail = dvTypeFilter.ToTable();
                }
               
                //binding the ddl RM
                rcbHead.DataSource = dtRMDetail;
                rcbHead.DataTextField = "name";
                rcbHead.DataValueField = "ar_rmid";
                rcbHead.DataBind();

                //BindAreaName(advisorVo.advisorId, zoneId);
                
                //binding ddl Zone
                rcbPickAZone.DataSource = dtZoneDetail;
                rcbPickAZone.DataTextField = "AZOC_Name";
                rcbPickAZone.DataValueField = "AZOC_ZoneClusterId";
                rcbPickAZone.DataBind();

                //rcbPickArea.DataSource = dtZoneDetail;
                //rcbPickArea.DataTextField = "AZOC_Name";
                //rcbPickArea.DataValueField = "AZOC_ZoneClusterId";
                //rcbPickArea.DataBind();

                rcbHead.Items.Insert(0, defaultItem);
                rcbPickAZone.Items.Insert(0, defaultItemPickAZone);
                if (rcbEditFormAddType.SelectedValue == "2")
                {

                    trPickAZone.Visible = true;
                    trPickArea.Visible = false;
                    trPickAType.Visible = true;
                }

                else if (rcbEditFormAddType.SelectedValue == "3")
                {
                    trPickAZone.Visible = true;
                    trPickArea.Visible = false;
                    trPickAType.Visible = false;
                }
                else
                {
                    trPickAZone.Visible = false;
                    trPickArea.Visible = false;
                    trPickAType.Visible = false;
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
                FunctionInfo.Add("Method", "AdviserZoneCluster.ascx:rcbEditFormAddType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)");
                object[] objects = new object[3];
                objects[0] = sender;
                objects[1] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        /// This method will bind the zone/cluster details grid
        /// </summary>
        /// <param name="adviserId">This is a filter in accordance to the adviserid</param>
        /// <param name="type">This is a type filter if it is a Zone or cluster or Both</param>
        public void BindZoneClusterGrid(int adviserId, int type)
        {
            try
            {

                dsZoneClusterDetails = new DataSet();
                advisorBo = new AdvisorBo();
                //getting dataset for grid and the rm ddl and the zone name ddl
                dsZoneClusterDetails = advisorBo.GetZoneClusterDetailsAdviserwise(advisorVo.advisorId, Convert.ToInt32(rcbShow.SelectedValue));
                gvZoneClusterDetails.DataSource = dsZoneClusterDetails;
                gvZoneClusterDetails.DataBind();

                //set the visibility for export button
                if (dsZoneClusterDetails != null)
                    btnExportFilteredData.Visible = true;

                //storing the rm  data into cache no going to the database again while performing insert or update
                if (Cache["RMDetail" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("RMDetail" + advisorVo.advisorId, dsZoneClusterDetails.Tables[1]);
                }
                else
                {
                    Cache.Remove("RMDetail" + advisorVo.advisorId);
                    Cache.Insert("RMDetail" + advisorVo.advisorId, dsZoneClusterDetails.Tables[1]);
                }


                //storing the ZoneDetails ddl data into cache no going to the database again while performing insert or update
                if (Cache["ZoneDetail" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("ZoneDetail" + advisorVo.advisorId, dsZoneClusterDetails.Tables[2]);
                }
                else
                {
                    Cache.Remove("ZoneDetail" + advisorVo.advisorId);
                    Cache.Insert("ZoneDetail" + advisorVo.advisorId, dsZoneClusterDetails.Tables[2]);
                }


                //storing the zoneclusterdetails data into cache no going to the database again while performing grid operations
                if (Cache["ZoneClusterDetails" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("ZoneClusterDetails" + advisorVo.advisorId, dsZoneClusterDetails.Tables[0]);
                }
                else
                {
                    Cache.Remove("ZoneClusterDetails" + advisorVo.advisorId);
                    Cache.Insert("ZoneClusterDetails" + advisorVo.advisorId, dsZoneClusterDetails.Tables[0]);
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
                FunctionInfo.Add("Method", "AdviserZoneCluster.ascx:BindZoneClusterGrid(int adviserId, int type)");
                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = type;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        /// This method will perform the command of the radgrid insert , edit or deleted 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void gvZoneClusterDetails_ItemCommand(object source, GridCommandEventArgs e)
        {
            string description = string.Empty;
            string name = string.Empty;
            string insertType = string.Empty;

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int AZOC_ZoneId = 0;
                advisorBo = new AdvisorBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                RadComboBox rcbEditFormAddType = (RadComboBox)e.Item.FindControl("rcbEditFormAddType");
                TextBox txtName = (TextBox)e.Item.FindControl("txtName");
                RadComboBox rcbHead = (RadComboBox)e.Item.FindControl("rcbHead");
                RadComboBox rcbPickAZone = (RadComboBox)e.Item.FindControl("rcbPickAZone");
                TextBox txtDescription = (TextBox)e.Item.FindControl("txtDescription");
                RadComboBox rcbPickArea = (RadComboBox)e.Item.FindControl("rcbPickArea");
                  RadComboBox rcbEditForm = (RadComboBox)e.Item.FindControl("rcbEditForm");
                string AZOC_Type = gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_Type"].ToString();

                if (AZOC_Type != "Zone")
                {
                    if (rcbEditForm.SelectedValue == "1")
                    {
                        AZOC_ZoneId = Convert.ToInt32(rcbPickAZone.SelectedValue);
                    }
                    else
                    {
                        AZOC_ZoneId = Convert.ToInt32(rcbPickArea.SelectedValue);
                    }
                }

                string zoneName = txtName.Text;
                int AZOC_ZoneClusterId = Convert.ToInt32(gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_ZoneClusterId"].ToString());
                type = Convert.ToInt32(rcbEditFormAddType.SelectedValue);
                if (type == 1)
                    insertType = "Zone";
                else if (type == 2)
                    insertType = "Cluster";
                else
                    insertType = "Area";
                //check if update then show the message
                isUpdated = advisorBo.ZoneClusterDetailsAddEditDelete(advisorVo.advisorId, Convert.ToInt32(rcbHead.SelectedValue), AZOC_ZoneClusterId, AZOC_ZoneId, txtDescription.Text, txtName.Text, insertType, 0, UserVo.UserId, DateTime.MinValue, DateTime.Now, e.CommandName);
                if (isUpdated == false)
                    Response.Write(@"<script language='javascript'>alert('The error updating Zone : \n" + zoneName + "');</script>");
                else
                    if (type == 1)
                    {
                        Response.Write(@"<script language='javascript'>alert('The Zone: \n" + zoneName + " updated successfully.');</script>");
                    }
                    else
                    {
                        Response.Write(@"<script language='javascript'>alert('The Cluster: \n" + zoneName + " updated successfully.');</script>");
                    }
                BindZoneClusterGrid(advisorVo.advisorId, type);
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                advisorBo = new AdvisorBo();
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                RadComboBox rcbEditFormAddType = (RadComboBox)e.Item.FindControl("rcbEditFormAddType");
                TextBox txtName = (TextBox)e.Item.FindControl("txtName");
                RadComboBox rcbHead = (RadComboBox)e.Item.FindControl("rcbHead");
                TextBox txtDescription = (TextBox)e.Item.FindControl("txtDescription");
                RadComboBox rcbPickAZone = (RadComboBox)e.Item.FindControl("rcbPickAZone");
                RadComboBox rcbPickArea = (RadComboBox)e.Item.FindControl("rcbPickArea");
                RadComboBox rcbEditForm = (RadComboBox)e.Item.FindControl("rcbEditForm");
                type = Convert.ToInt32(rcbEditFormAddType.SelectedValue);
                if (string.IsNullOrEmpty(rcbHead.SelectedValue))
                    rcbHead.SelectedValue = "0";
                if (type == 1)
                {
                    insertType = "Zone";
                    //check if inserted then show message
                    isInserted = advisorBo.ZoneClusterDetailsAddEditDelete(advisorVo.advisorId, Convert.ToInt32(rcbHead.SelectedValue), Convert.ToInt32(0), 0, txtDescription.Text, txtName.Text, insertType, UserVo.UserId, 0, DateTime.Now, DateTime.MinValue, e.CommandName);

                }
                else if (type == 2)
                {
                    insertType = "Cluster";
                    //check if inserted then show message
                    if (rcbEditForm.SelectedValue == "1")
                    {
                        isInserted = advisorBo.ZoneClusterDetailsAddEditDelete(advisorVo.advisorId, Convert.ToInt32(rcbHead.SelectedValue), Convert.ToInt32(rcbPickAZone.SelectedValue), 0, txtDescription.Text, txtName.Text, insertType, UserVo.UserId, 0, DateTime.Now, DateTime.MinValue, e.CommandName);
                    }
                    else
                        {
                            isInserted = advisorBo.ZoneClusterDetailsAddEditDelete(advisorVo.advisorId, Convert.ToInt32(rcbHead.SelectedValue), Convert.ToInt32(rcbPickArea.SelectedValue), 0, txtDescription.Text, txtName.Text, insertType, UserVo.UserId, 0, DateTime.Now, DateTime.MinValue, e.CommandName);
                        }
                }
                else
                {
                    insertType = "Area";
                    //check if inserted then show message
                    isInserted = advisorBo.ZoneClusterDetailsAddEditDelete(advisorVo.advisorId, Convert.ToInt32(rcbHead.SelectedValue), Convert.ToInt32(rcbPickAZone.SelectedValue), 0, txtDescription.Text, txtName.Text, insertType, UserVo.UserId, 0, DateTime.Now, DateTime.MinValue, e.CommandName);
                }
                if (isInserted == false)
                    Response.Write(@"<script language='javascript'>alert('Error inserting records');</script>");
                else
                    Response.Write(@"<script language='javascript'>alert('Records inserted successfully');</script>");
                BindZoneClusterGrid(advisorVo.advisorId, type);
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDeleted = false;
                advisorBo = new AdvisorBo();
                GridDataItem dataItem = (GridDataItem)e.Item;
                TableCell ZoneClusterIdForDelete = dataItem["AZOC_ZoneClusterId"];
                string zoneName = gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_Name"].ToString();
                int zoneId = int.Parse(ZoneClusterIdForDelete.Text);
                string deleteType = gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_Type"].ToString();
                //check if deleted then show message
                isDeleted = advisorBo.ZoneClusterDetailsAddEditDelete(advisorVo.advisorId, 0, zoneId, 0, string.Empty, string.Empty, deleteType.ToString(), UserVo.UserId, 0, DateTime.Now, DateTime.MinValue, e.CommandName);
                if (isDeleted == false)
                    Response.Write(@"<script language='javascript'>alert('The Zone : \n" + zoneName + " Cannot be deleted since it is attached to a Zone.');</script>");
                else
                    Response.Write(@"<script language='javascript'>alert('The Zone: \n" + zoneName + " deleted successfully.');</script>");
                BindZoneClusterGrid(advisorVo.advisorId, type);
            }
            //bind the grid to get the edit form mode
           
        }

        /// <summary>
        /// This method is used to fill the data in the grid when the data are needed to perform grid operation 
        /// such as paging , filter etc
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void gvZoneClusterDetails_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGvSchemeDetails = new DataTable();
            try
            {
                dtGvSchemeDetails = (DataTable)Cache["ZoneClusterDetails" + advisorVo.advisorId];
                gvZoneClusterDetails.DataSource = dtGvSchemeDetails;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserZoneCluster.ascx:gvZoneClusterDetails_NeedDataSource(object source, GridNeedDataSourceEventArgs e)");
                object[] objects = new object[3];
                objects[0] = dtGvSchemeDetails;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        /// This method will bound the grid edit form items with the data extracted from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvZoneClusterDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            dsRMAndZoneDetails = new DataSet();
            advisorBo = new AdvisorBo();

            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DataTable dtAccType = new DataTable();
                DataTable dtZoneDetail = new DataTable();
                DataTable dtRMDetail = new DataTable();

                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                RadComboBox rcbHead = (RadComboBox)gefi.FindControl("rcbHead");
                RadComboBox rcbPickAZone = (RadComboBox)gefi.FindControl("rcbPickAZone");
                RadComboBox rcbPickArea = (RadComboBox)gefi.FindControl("rcbPickAZone");

                //getting the ZoneDetail from cache 
                dtZoneDetail = (DataTable)Cache["ZoneDetail" + advisorVo.advisorId];

                //getting the RMDetail from cache                
                dtRMDetail = (DataTable)Cache["RMDetail" + advisorVo.advisorId];

                RadComboBoxItem defaultItem = new RadComboBoxItem();
                //setting default for radcombobox
                defaultItem.Text = "";
                defaultItem.Value = "";
           
                  
                
                //binding the ddl RM
                //rcbHead.DataSource = dtRMDetail;
                //rcbHead.DataTextField = "name";
                //rcbHead.DataValueField = "ar_rmid";
                //rcbHead.Items.Insert(0, defaultItem);
                //rcbHead.DataBind();

                //binding ddl Zone
                //rcbPickAZone.DataSource = dtZoneDetail;
                //rcbPickAZone.DataTextField = "AZOC_Name";
                //rcbPickAZone.DataValueField = "AZOC_ZoneClusterId";
                //rcbHead.Items.Insert(0, defaultItem);
                //rcbPickAZone.DataBind();
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                //finding data key names for type , name and zone to fill the ddl respectively
                string type = gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_Type"].ToString();
                int Name = 0;
                int AZOC_ZoneId = 0;
                //if (!string.IsNullOrEmpty(gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RMId"].ToString()))
                //    Name = Convert.ToInt32(gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RMId"].ToString());
                int zoneName = Convert.ToInt32(gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_ZoneClusterId"].ToString());
                if (!string.IsNullOrEmpty(gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_ZoneId"].ToString()))
                    AZOC_ZoneId = Convert.ToInt32(gvZoneClusterDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AZOC_ZoneId"].ToString());
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;

                //finding control for ddl type , head and Zone
                RadComboBox rcbEditFormAddType = (RadComboBox)e.Item.FindControl("rcbEditFormAddType");
                RadComboBox rcbHead = (RadComboBox)e.Item.FindControl("rcbHead");
                RadComboBox rcbPickAZone = (RadComboBox)e.Item.FindControl("rcbPickAZone");
                TextBox txtName = (TextBox)e.Item.FindControl("txtName");

                
                HtmlTableRow trPickAZone = editedItem.FindControl("trPickAZone") as HtmlTableRow;
                RadComboBox rcbPickArea = (RadComboBox)e.Item.FindControl("rcbPickArea");
                HtmlTableRow trPickArea = editedItem.FindControl("trPickArea") as HtmlTableRow;
                RadComboBox rcbEditForm = (RadComboBox)e.Item.FindControl("rcbEditForm");
                HtmlTableRow trPickAType = editedItem.FindControl("trPickAType") as HtmlTableRow;
                DataTable dtZoneDetail = new DataTable();
                DataTable dtRMDetail = new DataTable();

                //disabling ddl for type update we will not allow changing the zone to cluster or vice versa
                rcbEditFormAddType.Enabled = false;
                trPickAType.Visible = false;

                //getting the ZoneDetail from cache 
                dtZoneDetail = (DataTable)Cache["ZoneDetail" + advisorVo.advisorId];

                //getting the RMDetail from cache                
                dtRMDetail = (DataTable)Cache["RMDetail" + advisorVo.advisorId];

                //hiding the zone details when the zone type is shown or vice versa
                if (type == "Zone")
                {
                    type = "1";
                    trPickAZone.Visible = false;
                    trPickAType.Visible = false;
                    string JobFunctionFilter;
                    string typeFilter;
                    JobFunctionFilter = "AR_JobFunction=" + "'Zonal Head'";
                    typeFilter = "AZOC_Type=" + "'Zone'";
                    //dataview creation for job function
                    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, JobFunctionFilter, "AR_JobFunction", DataViewRowState.CurrentRows);
                    dtRMDetail = dvMFTransactionsProcessed.ToTable();
                    //dataview creation for type filter
                    DataView dvTypeFilter = new DataView(dtZoneDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
                    dtZoneDetail = dvTypeFilter.ToTable();
                }
                else if (type == "Cluster")
                {
                    type = "2";
                    trPickAZone.Visible = true;
                    trPickAType.Visible = true;
                    string expressionForRowsWithoutFM;
                    string typeFilter;
                    expressionForRowsWithoutFM = "AR_JobFunction=" + "'Cluster Head'";
                    typeFilter = "AZOC_Type=" + "'Zone'";
                    //dataview creation for job function
                    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, expressionForRowsWithoutFM, "AR_JobFunction", DataViewRowState.CurrentRows);
                    dtRMDetail = dvMFTransactionsProcessed.ToTable();
                    //dataview creation for type filter
                    DataView dvTypeFilter = new DataView(dtZoneDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
                    dtZoneDetail = dvTypeFilter.ToTable();
                }
                else
                {
                    type = "3";
                    trPickAZone.Visible = true;
                    trPickAType.Visible = false;
                    string expressionForRowsWithoutFM;
                    string typeFilter;
                    expressionForRowsWithoutFM = "AR_JobFunction=" + "'Area Head'";
                    typeFilter = "AZOC_Type=" + "'Zone'";
                    //dataview creation for job function
                    DataView dvMFTransactionsProcessed = new DataView(dtRMDetail, expressionForRowsWithoutFM, "AR_JobFunction", DataViewRowState.CurrentRows);
                    dtRMDetail = dvMFTransactionsProcessed.ToTable();
                    //dataview creation for type filter
                    DataView dvTypeFilter = new DataView(dtZoneDetail, typeFilter, "AZOC_Type", DataViewRowState.CurrentRows);
                    dtZoneDetail = dvTypeFilter.ToTable();
                }



                //filling the data for zone ddl
                rcbPickAZone.DataSource = dtZoneDetail;
                rcbPickAZone.DataTextField = "AZOC_Name";
                rcbPickAZone.DataValueField = "AZOC_ZoneClusterId";
                rcbPickAZone.DataBind();
             
                    
                //rcbPickArea.DataSource = dtZoneDetail;
                //rcbPickArea.DataTextField = "AZOC_Name";
                //rcbPickArea.DataValueField = "AZOC_ZoneClusterId";
                //rcbPickArea.DataBind();

                //filling the data from rm ddl
                rcbHead.DataSource = dtRMDetail;
                rcbHead.DataTextField = "name";
                rcbHead.DataValueField = "ar_rmid";
                rcbHead.DataBind();
               
                //setting the value in the ddl according to the grid dataitem 
                rcbEditFormAddType.SelectedValue = type.ToString();
                rcbHead.SelectedValue = Name.ToString();

                //setting the zone Id or the cluster in the same ddl
                if (AZOC_ZoneId != 0)
                    rcbPickAZone.SelectedValue = AZOC_ZoneId.ToString();
                else
                    rcbPickAZone.SelectedValue = zoneName.ToString();
            }
        }

        /// <summary>
        /// This method will export data into the excel file from the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        //protected void BindAreaName(int adviserId, int zoneClusterId)
        //{
           
        //    RadComboBox item = new RadComboBox();
        //    dsGetArea = advisorBo.BindArea(adviserId, zoneClusterId);
        //    dtGetArea = dsGetArea.Tables[0];
        //    rcbPickArea.DataSource = dtGetArea;
        //    rcbPickArea.DataValueField = dtGetArea.Columns["AZOC_ZoneClusterId"].ToString();
        //    rcbPickArea.DataTextField = dtGetArea.Columns["AZOC_Name"].ToString();
        //    rcbPickArea.DataBind();
           
        //}
        protected void rcbEditForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsGetArea;
            DataTable dtGetArea = new DataTable();
            AdvisorBo advisorBo = new AdvisorBo();
            RadComboBox RadComboBox = (RadComboBox)sender;
            GridEditableItem editedItem = RadComboBox.NamingContainer as GridEditableItem;
            RadComboBox rcbEditFormAddType = editedItem.FindControl("rcbEditFormAddType") as RadComboBox;
            HtmlTableRow trPickAZone = editedItem.FindControl("trPickAZone") as HtmlTableRow;
            RadComboBox rcbHead = editedItem.FindControl("rcbHead") as RadComboBox;
            RadComboBox rcbPickAZone = editedItem.FindControl("rcbPickAZone") as RadComboBox;
            HtmlTableRow trPickArea = editedItem.FindControl("trPickArea") as HtmlTableRow;
            RadComboBox rcbPickArea = editedItem.FindControl("rcbPickArea") as RadComboBox;
            RadComboBox rcbEditForm = editedItem.FindControl("rcbEditForm") as RadComboBox;
            HtmlTableRow trPickAType = editedItem.FindControl("trPickAType") as HtmlTableRow;
            dtGetArea = (DataTable)Cache["ZoneDetail" + advisorVo.advisorId];
            trPickAZone.Visible = true;
            rcbPickAZone.Visible = true;
            if (rcbEditForm.SelectedValue == "3")
            {
                //int zoneClusterId = rcbPickAZone.FindItemByValue(dr["AZOC_ZoneClusterId"].ToString());
                trPickArea.Visible = true;
                rcbPickArea.Visible = true;
               
                int adviserId = advisorVo.advisorId;
                dsGetArea = advisorBo.BindArea(adviserId);
                dtGetArea = dsGetArea.Tables[0];
                rcbPickArea.DataSource = dtGetArea;
                rcbPickArea.DataValueField = dtGetArea.Columns["AZOC_ZoneClusterId"].ToString();
                rcbPickArea.DataTextField = dtGetArea.Columns["AZOC_Name"].ToString();
                rcbPickArea.DataBind();
                trPickAZone.Visible = false;
                rcbPickAZone.Visible = false;
            }
            else
            {
                trPickArea.Visible = false;
                rcbPickArea.Visible = false;
                int adviserId = advisorVo.advisorId;
                dsGetArea = advisorBo.BindArea(adviserId);
                dtGetArea = dsGetArea.Tables[1];
                rcbPickAZone.DataSource = dtGetArea;
                rcbPickAZone.DataTextField = dtGetArea.Columns["AZOC_Name"].ToString();
                rcbPickAZone.DataValueField = dtGetArea.Columns["AZOC_ZoneClusterId"].ToString();
                rcbPickAZone.DataBind();
               
            }

        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtExportFilteredData = new DataSet();
            try
            {
                //getting the dataset from the cache
                dtExportFilteredData = (DataSet)Cache["gvZoneClusterDetails"];
                gvZoneClusterDetails.DataSource = dtExportFilteredData;

                gvZoneClusterDetails.ExportSettings.OpenInNewWindow = true;
                gvZoneClusterDetails.ExportSettings.IgnorePaging = true;
                gvZoneClusterDetails.ExportSettings.HideStructureColumns = true;
                gvZoneClusterDetails.ExportSettings.ExportOnlyData = true;
                gvZoneClusterDetails.ExportSettings.FileName = "Zone Cluster Details";
                gvZoneClusterDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvZoneClusterDetails.MasterTableView.ExportToExcel();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserZoneCluster.ascx:btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)");
                object[] objects = new object[3];
                objects[0] = dtExportFilteredData;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}