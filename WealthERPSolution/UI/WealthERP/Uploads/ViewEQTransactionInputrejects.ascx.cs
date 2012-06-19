using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUploads;
using VoUser;
using VoUploads;
using BoUploads;
using BoCommon;
using WealthERP.Base;
using System.Configuration;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web;


namespace WealthERP.Uploads
{

    public partial class ViewEQTransactionInputrejects : System.Web.UI.UserControl
    {
        RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();
        int processId;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserVo userVo = (UserVo)Session[SessionContents.UserVo];

            SessionBo.CheckSession();

            if (!IsPostBack)
            {
                if (Request.QueryString["processId"] != null)
                {
                    processId = Int32.Parse(Request.QueryString["processId"].ToString());
                }
                else
                    processId = 0;
                bindEQInputGrid();

            }
        }

        public void bindEQInputGrid()
        {
            DataSet dsRejectedRecords;
            dsRejectedRecords = rejectedTransactionsBo.GETAllEquityInputRejectTransactions(processId);
            gvEquityInputRejects.DataSource = dsRejectedRecords.Tables[0];
            gvEquityInputRejects.DataBind();

            if (Cache["gvEquityInputRejectsList"] == null)
            {
                Cache.Insert("gvEquityInputRejectsList", dsRejectedRecords);
            }
            else
            {
                Cache.Remove("gvEquityInputRejectsList");
                Cache.Insert("gvEquityInputRejectsList", dsRejectedRecords);
            }

                        


        }

        protected void gvEquityInputRejects_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtgvEquityInputRejectsList = new DataSet();
            dtgvEquityInputRejectsList = (DataSet)Cache["gvEquityInputRejectsList"];
            gvEquityInputRejects.DataSource = dtgvEquityInputRejectsList;

        }
    }
}