using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserControlDelegate
{
    public partial class _Default : System.Web.UI.Page
    {
        private string _ucPath;

        public delegate void delAddUc(string ucPathref);


        public string ucPath
        {
            get
            {
                return _ucPath;
            }
            set
            {
                _ucPath = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            delAddUc addUc = new delAddUc(AddUserControl);
            if (!Page.IsPostBack)
            {
                this.ucPath = "UserControl/ucHome.ascx";
                this.lblUcPath.Text = "UserControl/ucHome.ascx";
                addUc(this.ucPath);

            }
            else
            {
                
                this.UCHeader1.setUcPath = addUc;

            }
        
        }
        public void AddUserControl(string ucPath)
        {
            this.placeholder.Controls.Clear();

            Control uc = new Control();
            uc = LoadControl(ucPath);
            uc.ID = ucPath;
            this.placeholder.Controls.Add(uc);

        }
    }
}
