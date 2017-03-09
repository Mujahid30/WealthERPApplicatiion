using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP
{
    public partial class Pager : System.Web.UI.UserControl
    {
        private int _currentPage = 1;
        private int _pageCount = 1;

        public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);
        public event ItemClickEventHandler ItemClicked;

        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPageNo.Attributes.Add("text-align", "center");
                Set_Page(int.Parse(currentPage.Value), int.Parse(pageCount.Value));
            }
        }

        public void Set_Page(int curr, int max)
        {
            if (curr <= 1)
            {
                btnPrev.Enabled = false;
                btnFirst.Enabled = false;
                btnPrev.ImageUrl = "~/Images/ImgPrevMD.gif";
                btnFirst.ImageUrl = "~/Images/ImgFirstMD.gif";
            }
            else
            {
                btnPrev.Enabled = true;
                btnFirst.Enabled = true;
                btnPrev.ImageUrl = "~/Images/ImgPrevMN.gif";
                btnFirst.ImageUrl = "~/Images/ImgFirstMN.gif";
            }
            if (curr >= max)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnNext.ImageUrl = "~/Images/ImgNextMD.gif";
                btnLast.ImageUrl = "~/Images/ImgLastMD.gif";
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                btnNext.ImageUrl = "~/Images/ImgNextMN.gif";
                btnLast.ImageUrl = "~/Images/ImgLastMN.gif";
            }

            txtPageNo.Text = string.Format("{0} of {1}", curr, max);
            currentPage.Value = curr.ToString();
            pageCount.Value = max.ToString();
            CurrentPage = curr;
        }

        protected void btnPrev_Click(object sender, ImageClickEventArgs e)
        {
            int cpage;

            if (int.TryParse(currentPage.Value, out cpage) == false)
                cpage = 1;

            cpage--;

            if (cpage <= 1)
                cpage = 1;

            Set_Page(cpage, Convert.ToInt32(pageCount.Value));

            if (this.ItemClicked != null)
            {
                ItemClickEventArgs arg = new ItemClickEventArgs();
                arg.PageNo = cpage;
                this.ItemClicked(this, arg);
            }
        }


        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int cpage, max = Convert.ToInt32(pageCount.Value);

            if (int.TryParse(currentPage.Value, out cpage) == false)
                cpage = 1;

            cpage++;

            if (cpage > max)
                cpage = max;

            Set_Page(cpage, Convert.ToInt32(pageCount.Value));

            if (this.ItemClicked != null)
            {
                ItemClickEventArgs arg = new ItemClickEventArgs();
                arg.PageNo = cpage;
                this.ItemClicked(this, arg);
            }
        }

        protected void btnFirst_Click(object sender, ImageClickEventArgs e)
        {
            Set_Page(1, Convert.ToInt32(pageCount.Value));

            if (this.ItemClicked != null)
            {
                ItemClickEventArgs arg = new ItemClickEventArgs();
                arg.PageNo = 1;
                this.ItemClicked(this, arg);
            }
        }

        protected void btnLast_Click(object sender, ImageClickEventArgs e)
        {
            Set_Page(Convert.ToInt32(pageCount.Value), Convert.ToInt32(pageCount.Value));

            if (this.ItemClicked != null)
            {
                ItemClickEventArgs arg = new ItemClickEventArgs();
                arg.PageNo = Convert.ToInt32(pageCount.Value);
                this.ItemClicked(this, arg);
            }
        }

        protected void txtPageNo_TextChanged(object sender, EventArgs e)
        {
            int pageRequest, pagecount = 0;
            int.TryParse(pageCount.Value, out pagecount);
            int.TryParse(txtPageNo.Text, out pageRequest);

            if (pageRequest >= 1 && pageRequest <= pagecount)
            {
                Set_Page(pageRequest, pagecount);

                if (this.ItemClicked != null)
                {
                    ItemClickEventArgs arg = new ItemClickEventArgs();
                    arg.PageNo = pageRequest;
                    this.ItemClicked(this, arg);
                }
            }
            else
            {
                txtPageNo.Text = currentPage.Value + " of " + pageCount.Value;
                return;
            }
        }
    }

    public class ItemClickEventArgs : EventArgs
    {
        private int _pageRequest;

        public int PageNo
        {
            get { return _pageRequest; }
            set { _pageRequest = value; }
        }
    }
}
