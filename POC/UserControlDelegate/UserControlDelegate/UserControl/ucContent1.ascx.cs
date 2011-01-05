using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserControlDelegate.UserControl
{
    public partial class ucContent1 : System.Web.UI.UserControl
    {
        private System.Delegate _setUcPath;
        private string _ucPath;
        public System.Delegate setUcPath
        {
            get
            {
                return _setUcPath;
            }

            set
            {
                _setUcPath = value;
            }

        }

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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.ucPath = "UserControl/ucContent2.ascx";
            getUcpath();
        }
        void getUcpath()
        {
            object[] obj;
            obj = new object[2];
            //obj = null;
            obj[0] = _ucPath;
            setUcPath.DynamicInvoke(obj[0]);

        }
    }
}