using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoUser;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AdviserAssociateCategorySetup : System.Web.UI.UserControl
    {

        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();

        bool DataBoundedFromDb = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];


            if (!Page.IsPostBack)
            {
                
                FillGrid();
                //SetInitialRow();

            }
            if (gvAssocCatSetUpBounded.Rows.Count > 0)
                DataBoundedFromDb = true;

           
        }

        private void SetInitialRow()
        {

            DataTable dt = new DataTable();

            
            int num = Convert.ToInt16(txtNoOfCat.Text.ToString());

            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("AssociateCategoryCode", typeof(string)));
            dt.Columns.Add(new DataColumn("AssociateCategoryName", typeof(string)));

            
            for (int i = 1; i <= num; i++)
            {
                DataRow dr = null;
                dr = dt.NewRow();
                dr["RowNumber"] = i;


                int rowIndex = 0;

                dr["AssociateCategoryCode"] = string.Empty;
                //TextBox box2 = (TextBox)gvAssocCatSetUp.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                //box2.Text = Category + i;
                dr["AssociateCategoryName"] = string.Empty;
                dt.Rows.Add(dr);
                rowIndex++;
                
            }


            //for (int rowIndex = 0; rowIndex < gvAssocCatSetUp.Rows.Count; rowIndex++)
            //{

            //}
            

            ViewState["CurrentTable"] = dt;



            gvAssocCatSetUp.DataSource = dt;

            gvAssocCatSetUp.DataBind();
            trAssignNumber.Visible = false;
            trMeaageDefault.Visible = true;

        }


        private void AddNewRowToGrid()
        {

            int rowIndex = 0;
            
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box1;
                        TextBox box2;
                        //extract the TextBox values
                        if (DataBoundedFromDb)
                        {
                             box1 = (TextBox)gvAssocCatSetUpBounded.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                             box2 = (TextBox)gvAssocCatSetUpBounded.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        }
                        else
                        {
                             box1 = (TextBox)gvAssocCatSetUp.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                             box2 = (TextBox)gvAssocCatSetUp.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        }
  
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["AssociateCategoryCode"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["AssociateCategoryName"] = box2.Text;
                        rowIndex++;
                    }

                    

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    if (DataBoundedFromDb)
                    {
                        gvAssocCatSetUpBounded.DataSource = dtCurrentTable;
                        gvAssocCatSetUpBounded.DataBind();
                    }
                    else
                    {
                        gvAssocCatSetUp.DataSource = dtCurrentTable;
                        gvAssocCatSetUp.DataBind();
                    }
                   

                }
            }
            else
            {
                Response.Write("ViewState is null");

            }

            //Set Previous Data on Postbacks
            SetPreviousData();
            //gvAssocCatSetUp.DataBind();

        }

        //This is to refresh the grid with existing data when a new row is added
        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count-1; i++)
                    {
                        TextBox box1;
                        TextBox box2;
                        if (DataBoundedFromDb)
                        {
                             box1 = (TextBox)gvAssocCatSetUpBounded.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                             box2 = (TextBox)gvAssocCatSetUpBounded.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        }
                        else
                        {
                             box1 = (TextBox)gvAssocCatSetUp.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                             box2 = (TextBox)gvAssocCatSetUp.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        }
                        box1.Text = dt.Rows[i]["AssociateCategoryCode"].ToString();
                        box2.Text = dt.Rows[i]["AssociateCategoryName"].ToString();
                        rowIndex++;

                    }

                }

            }

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();

            
        }

        protected void BtnNoOfCat_Click(object sender, EventArgs e)
        {
            SetInitialRow();
            if (btnSave.Visible == false)
                btnSave.Visible = true;
        }

        private void FillGrid()
        {
            DataSet ds;
            int num = 0;
            AdviserAssociateCategorySetupBo AdviserAssociateCategorySetup = new AdviserAssociateCategorySetupBo();


            ds = AdviserAssociateCategorySetup.GetAdviserAssociateCategory(adviserVo.advisorId);
            num = ds.Tables[0].Rows.Count;
            
            //IF num is 0 then load grid with deafult values.
            if (num == 0)
            {
                trAssignNumber.Visible = true;
                trUnboundedgrid.Visible = true;
                
                btnSave.Visible = false;
            }
            else
            {
                ds.Tables[0].Columns.Add("RowNumber");
                int i = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    dr["RowNumber"] = i;
                    i++;
                }

                ViewState["CurrentTable"] = ds.Tables[0];
                trBoundedgrid.Visible = true;
                gvAssocCatSetUpBounded.DataSource = ds;
                gvAssocCatSetUpBounded.DataBind();
                DataBoundedFromDb = true;
            }


        }

        protected void gvAssocCatSetUp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                //e.Row.Cells[2].Text = "Prince";
                TextBox box2 = (TextBox)e.Row.Cells[2].FindControl("TextBox2");
                if(box2.Text == "")
                box2.Text = "Category" + e.Row.Cells[0].Text;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           AssociateCategoryVo associatecategory = new AssociateCategoryVo();
           AdviserAssociateCategorySetupBo AdviserAssociateCategorySetup = new AdviserAssociateCategorySetupBo();


           associatecategory.AdviserId = adviserVo.advisorId;

            
            if (gvAssocCatSetUpBounded.Rows.Count > 0)
            {
                foreach (GridViewRow dr in gvAssocCatSetUpBounded.Rows)
                {
                    associatecategory.AssociateCategoryCode = ((TextBox)dr.FindControl("TextBox1")).Text;
                    associatecategory.AssociateCategoryName = ((TextBox)dr.FindControl("TextBox2")).Text;

                    if (gvAssocCatSetUpBounded.DataKeys[dr.RowIndex].Value != System.DBNull.Value)
                    {
                        associatecategory.AssociateCategoryId = Convert.ToInt32(gvAssocCatSetUpBounded.DataKeys[dr.RowIndex].Value);

                    }
                    else
                    {
                        associatecategory.AssociateCategoryId = 0;
                        associatecategory.CreatedBy = userVo.UserId;
                        associatecategory.CreatedOn = DateTime.Now;
                    }
                    
                    associatecategory.ModifiedBy = userVo.UserId;
                    associatecategory.Modifiedon = DateTime.Now;

                    bool result = AdviserAssociateCategorySetup.UpdateAdviserAssociateCategory(associatecategory);
                    
                }
                FillGrid();
            }
            else if (gvAssocCatSetUp.Rows.Count > 0)
            {
                foreach (GridViewRow dr in gvAssocCatSetUp.Rows)
                {
                    associatecategory.AssociateCategoryCode = ((TextBox)dr.FindControl("TextBox1")).Text;
                    associatecategory.AssociateCategoryName = ((TextBox)dr.FindControl("TextBox2")).Text;
                    associatecategory.CreatedBy = userVo.UserId;
                    associatecategory.CreatedOn = DateTime.Now;
                    associatecategory.ModifiedBy = userVo.UserId;
                    associatecategory.Modifiedon = DateTime.Now;

                    bool result = AdviserAssociateCategorySetup.InsertAdviserAssociateCategory(associatecategory);
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);
                //FillGrid();
            }
        }

        protected void DeleteAssocCategory(object sender, GridViewDeleteEventArgs e)
        {
            int AssocCategoryId =0;
            int rowindexval = e.RowIndex;
            int count = 0;
            bool result = false;
            AdviserAssociateCategorySetupBo AdviserAssociateCategorySetup = new AdviserAssociateCategorySetupBo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();

            if (gvAssocCatSetUpBounded.DataKeys[rowindexval].Value != System.DBNull.Value)
            {
                AssocCategoryId = Convert.ToInt32(gvAssocCatSetUpBounded.DataKeys[rowindexval].Value);
            }

            count = advisorBranchBo.CheckAssociateBranchCategory(AssocCategoryId);

            if (count > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry, This category cannot be deleted as this is associated with one or more branches !');", true);
            }
            else
            {
                result = AdviserAssociateCategorySetup.DeleteAdviserAssociateCategory(AssocCategoryId);
            }

            if (result)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Deleted Successfully !');", true);
                FillGrid();
            }
        }
    }
}