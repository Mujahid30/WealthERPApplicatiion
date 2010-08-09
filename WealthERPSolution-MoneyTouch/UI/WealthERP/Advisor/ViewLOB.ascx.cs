using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using BoCommon;
using VoAdvisorProfiling;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Advisor
{
    public partial class ViewLOB : System.Web.UI.UserControl
    {
        int index;
        string LOBId;
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
       // List<AdvisorLOBVo> advisorLOBList = null;
        DataSet advisorLOBList = new DataSet();
        AdvisorLOBVo advisorLOBVo;
        
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                showLOBList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewLOB.ascx.cs:Page_Load()");
                object[] objects = new object[1];
                objects[0] = advisorVo;
               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void showLOBList()
        {
            string path = "";
            string classificationCode = "";
            try
            {
                advisorVo = (AdvisorVo)Session["advisorVo"];

                
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                
                advisorLOBList = advisorLOBBo.GetAdvisorLOBs(advisorVo.advisorId,null,null);

                if (advisorLOBList.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Visible = false;

                    DataTable dtAdvisorLOB = new DataTable();
                    dtAdvisorLOB.Columns.Add("SI.No");
                    dtAdvisorLOB.Columns.Add("LOBId");
                    dtAdvisorLOB.Columns.Add("Broker Name");
                    dtAdvisorLOB.Columns.Add("Business Type");
                    dtAdvisorLOB.Columns.Add("Identifier");
                    dtAdvisorLOB.Columns.Add("Identifier Type");

                    DataRow drAdvisorLOB;

                    for (int i = 0; i < advisorLOBList.Tables[0].Rows.Count; i++)
                    {
                        classificationCode = advisorLOBList.Tables[0].Rows[i]["XALC_LOBClassificationCode"].ToString();
                        drAdvisorLOB = dtAdvisorLOB.NewRow();

                        drAdvisorLOB[0] = (i + 1).ToString();
                        drAdvisorLOB[1] = advisorLOBList.Tables[0].Rows[i]["AL_LOBId"].ToString();
                        drAdvisorLOB[2] = advisorLOBList.Tables[0].Rows[i]["AL_OrgName"].ToString();
                        drAdvisorLOB[3] = XMLBo.GetLOBType(path, advisorLOBList.Tables[0].Rows[i]["XALC_LOBClassificationCode"].ToString());
                        if (classificationCode == "LDSA" || classificationCode == "LFIA" || classificationCode == "LIAG" || classificationCode == "LPAG" || classificationCode == "LREA")
                        {
                            drAdvisorLOB[4] = advisorLOBList.Tables[0].Rows[i]["AL_AgentNo"].ToString();
                            drAdvisorLOB[5] = "Agent No./Agency Code";
                        }
                        else
                        {
                            drAdvisorLOB[4] = advisorLOBList.Tables[0].Rows[i]["AL_Identifier"].ToString();
                            drAdvisorLOB[5] = advisorLOBList.Tables[0].Rows[i]["XALIT_IdentifierTypeCode"].ToString();
                        }
                        dtAdvisorLOB.Rows.Add(drAdvisorLOB);
                    }
                    if (dtAdvisorLOB.Rows.Count > 10)
                        gvLOBList.ShowFooter = false;
                    else
                        gvLOBList.ShowFooter = true;
                    gvLOBList.DataSource = dtAdvisorLOB;
                    gvLOBList.DataBind();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "LOB List is Empty..";
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

                FunctionInfo.Add("Method", "ViewLOB.ascx:showLOBList()");


                object[] objects = new object[4];
                objects[0] = advisorVo;
                objects[1] = path;
                objects[2] = advisorLOBVo;
                objects[3] = advisorLOBList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        //protected void gvLOBlist_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        index = Convert.ToInt32(e.CommandArgument);
        //        LOBId = gvLOBList.DataKeys[index].Value.ToString();
        //        Session["LOBId"] = LOBId;
        //        if (e.CommandName == "Edit")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditLOB','none');", true);
        //        }
        //        if (e.CommandName == "View")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOBDetails','none');", true);
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "ViewLOB.ascx:gvLOBlist_RowCommand()");


        //        object[] objects = new object[2];
        //        objects[0] = index;
        //        objects[1] = LOBId;


        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}

        protected void gvLOBList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvLOBList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                sortGridViewLOB(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                sortGridViewLOB(sortExpression, ASCENDING);
            }
        }

        private void sortGridViewLOB(string sortExpression, string direction)
        {
            string path = "";
            try
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                advisorVo = (AdvisorVo)Session["advisorVo"];

                advisorLOBList = advisorLOBBo.GetAdvisorLOBs(advisorVo.advisorId, null, null);
                if (advisorLOBList.Tables[0].Rows.Count > 0)
                {
                    DataTable dtAdvisorLOB = new DataTable();
                    dtAdvisorLOB.Columns.Add("SI.No");
                    dtAdvisorLOB.Columns.Add("LOBId");
                    dtAdvisorLOB.Columns.Add("Broker Name");
                    dtAdvisorLOB.Columns.Add("Business Type");
                    dtAdvisorLOB.Columns.Add("Identifier");
                    dtAdvisorLOB.Columns.Add("Identifier Type");

                    DataRow drAdvisorLOB;

                    for (int i = 0; i < advisorLOBList.Tables[0].Rows.Count; i++)
                    {
                        drAdvisorLOB = dtAdvisorLOB.NewRow();

                        drAdvisorLOB[0] = (i + 1).ToString();
                        drAdvisorLOB[1] = advisorLOBList.Tables[0].Rows[i]["AL_LOBId"].ToString();
                        drAdvisorLOB[2] = advisorLOBList.Tables[0].Rows[i]["AL_OrgName"].ToString();
                        drAdvisorLOB[3] = XMLBo.GetLOBType(path, advisorLOBList.Tables[0].Rows[i]["XALC_LOBClassificationCode"].ToString());
                        drAdvisorLOB[4] = advisorLOBList.Tables[0].Rows[i]["AL_Identifier"].ToString();
                        drAdvisorLOB[5] = advisorLOBList.Tables[0].Rows[i]["XALIT_IdentifierTypeCode"].ToString();
                        dtAdvisorLOB.Rows.Add(drAdvisorLOB);
                    }

                    DataView dv = new DataView(dtAdvisorLOB);
                    dv.Sort = sortExpression + direction;
                    gvLOBList.DataSource = dtAdvisorLOB;
                    gvLOBList.DataBind();
                }
                else
                {
                    gvLOBList.DataSource = null;
                    gvLOBList.Visible = false;
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
                FunctionInfo.Add("Method", "ViewLOB.ascx.cs:sortGridViewLOB()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = path;
                objects[2] = advisorLOBList;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvLOBList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            gvLOBList.PageIndex = e.NewPageIndex;
            gvLOBList.DataBind();
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

            string menu="";
            int selectedRow = 0;
            int LOBId = 0;
            try
            {
                DropDownList MyDropDownList = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
                selectedRow = gvr.RowIndex;
                LOBId= int.Parse(gvLOBList.DataKeys[selectedRow].Value.ToString());
                Session["LOBId"] = LOBId;                
                menu = MyDropDownList.SelectedItem.Value.ToString();
                if (menu == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditLOB','none');", true);
                    Session["LOBGridAction"] = "AdvisorLOBEdit";
                }
                else if (menu == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOBDetails','none');", true);
                    Session["LOBGridAction"] = "LOBView";
                }
                else if (menu == "Delete")
                {
                    advisorLOBBo.DeleteLOB(LOBId);
                    showLOBList();
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
                FunctionInfo.Add("Method", "ViewLOB.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[3];
                objects[0] = LOBId;
                objects[1] = selectedRow;
                objects[2] = LOBId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        

        }
    }
}