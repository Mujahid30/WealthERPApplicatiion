using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using BoCustomerProfiling;
using VoUser;
using VoCustomerProfiling;
using BoAdvisorProfiling;
using BoCommon;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;

namespace WealthERP.Customer
{
    public partial class CustomerAssociatesAdd : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        List<CustomerVo> customerList = null;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
        UserVo userVo = new UserVo();

        DataTable dtRelationship = new DataTable();
        int customerId;
        string path;

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

        protected override void OnInit(EventArgs e)
        {

            try
            {

                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAssociatesAdd.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindGrid(mypager.CurrentPage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAssociatesAdd.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }



        private void GetPageCount()
        {
            string upperlimit = "";
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = "";
            string PageRecords = "";
            try
            {
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
                ratio = rowCount / 15;
                mypager.PageCount = rowCount % 15 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 15) + 1).ToString();
                upperlimit = (mypager.CurrentPage * 15).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAssociatesAdd.ascx.cs:GetPageCount()");
                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userVo = (UserVo)Session[SessionContents.UserVo];

                if (!IsPostBack)
                    this.BindGrid(mypager.CurrentPage);
                if (customerVo.Type == "NIND")
                {
                    dtRelationship = XMLBo.GetRelationship(path, "NIND");
                    ddlRelationship.DataSource = dtRelationship;
                    ddlRelationship.DataTextField = "Relationship";
                    ddlRelationship.DataValueField = "RelationshipCode";
                    ddlRelationship.DataBind();
                }
                else
                {
                    dtRelationship = XMLBo.GetRelationship(path, "IND");
                    ddlRelationship.DataSource = dtRelationship;
                    ddlRelationship.DataTextField = "Relationship";
                    ddlRelationship.DataValueField = "RelationshipCode";
                    ddlRelationship.DataBind();
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
                FunctionInfo.Add("Method", "CustomerAssociatesAdd.ascx.cs:Page_Load()");
                object[] objects = new object[4];
                objects[0] = path;
                objects[1] = customerVo;
                objects[2] = userVo;
                objects[3] = dtRelationship;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        protected void BindGrid(int CurrentPage)
        {
            try
            {
                Dictionary<string, string> genDictParent = new Dictionary<string, string>();
                Dictionary<string, string> genDictCity = new Dictionary<string, string>();

                rmVo = (RMVo)Session["rmVo"];
                int count = 0;
                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                // advisorStaffBo.GetCustomerList(rmVo.RMId, "C").ToString();
                if (customerVo.RelationShip == "SELF")
                {
                    customerList = advisorStaffBo.GetCustomerForAssociation(customerVo.CustomerId, rmVo.RMId, mypager.CurrentPage, hdnSort.Value.ToString(), out count);
                }
                else
                {
                    customerList = null;
                }
                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                if (customerList == null)
                {
                    lblMessage.Text = "Customer already associated, cannot make more associations";
                    mypager.Visible = false;
                    btnAssociate.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    trRelationShip.Visible = false;
                }
                else
                {
                    trRelationShip.Visible = true;
                    lblMessage.Text = "Customer List";
                    lblMessage.CssClass = "HeaderText";
                    mypager.Visible = true;
                    btnAssociate.Visible = true;
                    lblCurrentPage.Visible = true;
                    lblTotalRows.Visible = true;
                    DataTable dtRMCustomer = new DataTable();

                    dtRMCustomer.Columns.Add("CustomerId");
                    dtRMCustomer.Columns.Add("AssociationId");

                    dtRMCustomer.Columns.Add("Customer Name");
                    dtRMCustomer.Columns.Add("Email");

                    DataRow drRMCustomer;


                    for (int i = 0; i < customerList.Count; i++)
                    {
                        drRMCustomer = dtRMCustomer.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerList[i];
                        drRMCustomer[0] = customerVo.CustomerId.ToString();
                        drRMCustomer[1] = customerVo.AssociationId.ToString();
                        drRMCustomer[2] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        drRMCustomer[3] = customerVo.Email.ToString();

                        dtRMCustomer.Rows.Add(drRMCustomer);
                    }
                    gvCustomers.DataSource = dtRMCustomer;
                    gvCustomers.DataBind();
                    this.GetPageCount();

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
                FunctionInfo.Add("Method", "CustomerAssociatesAdd.ascx:BindGrid()");
                object[] objects = new object[5];
                objects[0] = CurrentPage;
                objects[1] = rmVo;
                objects[2] = customerVo;
                objects[3] = customerList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }
        protected void gvCustomers_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                this.BindGrid(1);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                this.BindGrid(1);

            }
        }
        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            gvCustomers.DataBind();
        }

        protected void btnAssociate_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                foreach (GridViewRow gvr in this.gvCustomers.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        i = i + 1;
                    }
                }

                if (i == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select member to associate..!');", true);
                }
                else
                {
                    CustomerGroupAssociation();
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
                FunctionInfo.Add("Method", "CustomerAssociatesAdd.ascx:btnAssociate_Click()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = customerFamilyVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void CustomerGroupAssociation()
        {
            foreach (GridViewRow gvr in this.gvCustomers.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    customerFamilyVo.CustomerId = customerVo.CustomerId;
                    //   customerFamilyVo.AssociateCustomerId = int.Parse(gvCustomers.DataKeys[gvr.RowIndex].Value.ToString());
                    customerFamilyVo.AssociateCustomerId = int.Parse(gvCustomers.DataKeys[gvr.RowIndex].Values["CustomerId"].ToString());
                    customerFamilyVo.Relationship = ddlRelationship.SelectedItem.Value.ToString();
                    customerFamilyVo.AssociationId = int.Parse(gvCustomers.DataKeys[gvr.RowIndex].Values["AssociationId"].ToString());

                    customerFamilyBo.UpdateCustomerAssociate(customerFamilyVo, customerVo.CustomerId, userVo.UserId);
                    //customerFamilyBo.CreateCustomerFamily(customerFamilyVo, customerFamilyVo.CustomerId, userVo.UserId);
                }
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
        }

        //protected void btnFindAssociate_Click(object sender, EventArgs e)
        //{
        //    AdvisorBo adviserBo = new AdvisorBo();
        //    List<CustomerVo> customerList = new List<CustomerVo>();
        //    RMVo customerRMVo = new RMVo();

        //    Dictionary<string, string> genDictParent = new Dictionary<string, string>();
        //    Dictionary<string, string> genDictCity = new Dictionary<string, string>();

        //    try
        //    {
        //        rmVo = (RMVo)Session["rmVo"];
        //        lblTotalRows.Visible = false;
        //        lblCurrentPage.Visible = false;

        //        int count = 0;

        //        customerList = advisorStaffBo.FindCustomer(txtFindAssociate.Text.ToString(), rmVo.RMId, mypager.CurrentPage, out count, hdnSort.Value, "", "", "", "", "", out genDictParent, out genDictCity);

        //        if (customerList == null)
        //        {
        //            lblMessage.Visible = true;
        //            gvCustomers.Visible = false;
        //        }
        //        else
        //        {
        //            lblMessage.Visible = false;
        //            DataTable dtRMCustomer = new DataTable();

        //            dtRMCustomer.Columns.Add("CustomerId");
        //            dtRMCustomer.Columns.Add("AssociationId");
        //            dtRMCustomer.Columns.Add("Customer Name");
        //            dtRMCustomer.Columns.Add("Email");

        //            DataRow drRMCustomer;

        //            for (int i = 0; i < customerList.Count; i++)
        //            {
        //                drRMCustomer = dtRMCustomer.NewRow();
        //                customerVo = new CustomerVo();
        //                customerVo = customerList[i];
        //                drRMCustomer[0] = customerVo.CustomerId.ToString();
        //                drRMCustomer[1] = customerVo.AssociationId.ToString();
        //                drRMCustomer[2] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
        //                drRMCustomer[3] = customerVo.Email.ToString();

        //                dtRMCustomer.Rows.Add(drRMCustomer);
        //            }

        //            gvCustomers.DataSource = dtRMCustomer;
        //            gvCustomers.DataBind();
        //            this.GetPageCount();
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
        //        FunctionInfo.Add("Method", "CustomerAssociatesAdd.ascx.cs:btnFindAssociate_Click()");
        //        object[] objects = new object[3];
        //        objects[0] = rmVo;
        //        objects[1] = customerVo;
        //        objects[2] = customerList;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}
    }
}
