using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MyDatabaseApp
{
    public partial class _Default : System.Web.UI.Page
    {
        private bool _isSQLAuthentication;
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        public bool IsSQLAuthentication
        {
            get { return _isSQLAuthentication; }
            set { _isSQLAuthentication = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["_tempStatus"] != null)
                {
                    if (Session["_tempStatus"].ToString() == "1")
                    {
                        ConnectionString = Session["ConnectionString"].ToString();
                        SqlDataSource _sqltabledetails = new SqlDataSource();

                        _sqltabledetails.ConnectionString = ConnectionString;
                        _sqltabledetails.ProviderName = "System.Data.SqlClient";
                        _sqltabledetails.SelectCommand = "SELECT * FROM " + cmbTable.SelectedItem.Text;
                        RadGrid1.DataSource = _sqltabledetails;

                        RadGrid1.AutoGenerateColumns = true;
                       
                        RadGrid1.DataBind();
                    }
                }
            }
            
        }

        protected void chkAuthenticationType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuthenticationType.Checked == true)
            {
                lblUserName.Enabled = true;
                txtUserName.Enabled = true;
                lblPassword.Enabled = true;
                txtPassword.Enabled = true;             
                IsSQLAuthentication = true;
            }
            else
            {
                lblUserName.Enabled = false;
                txtUserName.Enabled = false;
                lblPassword.Enabled = false;
                txtPassword.Enabled = false;

                            
                IsSQLAuthentication = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {            
            lblTableName.Enabled = true;
            cmbTable.Enabled = true;
            btnGo.Enabled = true;
            Session["_tempStatus"] = "0";
            if (IsSQLAuthentication == true)
            {
                Session["ConnectionString"]=ConnectionString = "Server = " + txtServer.Text + "; Database = " + txtDatabase.Text + "; ID = " + txtUserName.Text + "; Password = " + txtPassword.Text+";Trusted_Connection=False";
            }
            else
            {
                 Session["ConnectionString"]=ConnectionString = "Server = "+txtServer.Text+"; Database = "+txtDatabase.Text+"; Trusted_Connection = True";
            }
            SqlDataSource _sqlDataSource = new SqlDataSource();
            _sqlDataSource.ConnectionString = ConnectionString;
            _sqlDataSource.SelectCommand = "SELECT name FROM sys.Tables ORDER BY name";
            
            //cmbTable.ItemsPerRequest = 10;
            //cmbTable.ShowMoreResultsBox = true;
            //cmbTable.EnableVirtualScrolling = true;
            DataSet dsTemp=GetDataSourceTableNames();
            cmbTable.DataSource = dsTemp;
            cmbTable.DataValueField = dsTemp.Tables[0].Columns["name"].ToString();
            cmbTable.DataTextField = dsTemp.Tables[0].Columns["name"].ToString();
            cmbTable.DataBind();
        }
        public DataSet GetDataSourceTableNames()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT name FROM sys.Tables ORDER BY name",ConnectionString);

            DataSet data = new DataSet();
            adapter.Fill(data);

            return data;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Session["_tempStatus"] = "1";
            ConnectionString = Session["ConnectionString"].ToString();
            SqlDataSource _sqltabledetails = new SqlDataSource();

            _sqltabledetails.ConnectionString = ConnectionString;
            _sqltabledetails.ProviderName = "System.Data.SqlClient";
            _sqltabledetails.SelectCommand = "SELECT * FROM " + cmbTable.SelectedItem.Text;
            RadGrid1.DataSource = _sqltabledetails;
            
            RadGrid1.AutoGenerateColumns = true;
            
            RadGrid1.DataBind();
        }
    }
}
