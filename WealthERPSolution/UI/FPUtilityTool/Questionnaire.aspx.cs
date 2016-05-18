using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoFPUtility;
using VOFPUtilityUser;
using System.Configuration;

namespace FPUtilityTool
{
    public partial class Questionnaire : System.Web.UI.Page
    {
        FPUserBO fpUserBo = new FPUserBO();
        FPUserVo userVo = new FPUserVo();
        protected void Page_Init(object sender, EventArgs e)
        {

            FPUserBO.CheckSession();
            userVo = (FPUserVo)Session["UserVo"];
            DataSet dsQuestionNOptions = fpUserBo.GetQuestionAndOptions(userVo.UserId);
            int adviserId = Convert.ToInt32(ConfigurationManager.AppSettings["ONLINE_ADVISER"]);
            DataSet dsGetquestionList = new DataSet();
            dsGetquestionList = fpUserBo.GetRiskProfileQuestion(adviserId);
            int questionNo = 0;
            int questioncount = 1;
            foreach (DataRow dr in dsGetquestionList.Tables[0].Rows)
            {
                questionNo = Convert.ToInt32(dr["QM_QuestionId"].ToString());
                PlaceHolder placeholder = new PlaceHolder();
                placeholder.Controls.Add(new LiteralControl("<div class=\"well\"><div class=\"row\"><div class=\"col-sm-2  form-group\"></div><div class=\"col-sm-8  form-group\"><p>" + dr["QM_Question"].ToString()
                    + "</p></div><div class=\"col-sm-2\"></div></div><div class=\"row\"><div class=\"col-sm-3\"></div><div class=\"col-sm-3\"></div><div class=\"col-sm-6\">"));
                CustomValidator customeValidator = new CustomValidator();
                customeValidator.ID = "CustomValidator" + questionNo.ToString();
                customeValidator.ErrorMessage = "Please select an option.";
                customeValidator.ValidationGroup = questionNo.ToString();
                customeValidator.ServerValidate += new ServerValidateEventHandler(cvRadioButtonGroup_ServerValidate);
                placeholder.Controls.Add(customeValidator);
                placeholder.Controls.Add(new LiteralControl("</div></div>"));

                DataSet ds = new DataSet();
                ds = fpUserBo.GetQuestionOption(Convert.ToInt32(dr["QM_QuestionId"].ToString()), adviserId);
                int optionNo = 1;
                foreach (DataRow droption in ds.Tables[0].Rows)
                {
                    optionNo = Convert.ToInt32(droption["QOM_OptionId"].ToString());
                    placeholder.Controls.Add(new LiteralControl("<div class=\"row\"><div class=\"col-sm-3  form-group\"></div><div class=\"col-sm-6  form-group\"><div class=\"radio radio-info radio-inline\">"));
                    RadioButton rbtn = new RadioButton();
                    rbtn.ID = "rbtn" + questionNo.ToString() + optionNo.ToString();
                    rbtn.GroupName = questionNo.ToString();
                    //rbtn.AccessKey = optionNo.ToString();
                    rbtn.ValidationGroup = optionNo.ToString();
                    if (dsQuestionNOptions.Tables[0].Rows.Count > 0)
                    {
                        string expression;
                        expression = "AQM_QuestionId=" + questionNo.ToString();
                        DataRow[] foundRows;
                        foundRows = dsQuestionNOptions.Tables[0].Select(expression);
                        if (foundRows.Length > 0)
                        {
                            if (foundRows[0]["AQOM_OptionId"].ToString() == optionNo.ToString())
                                rbtn.Checked = true;
                        }
                    }
                    placeholder.Controls.Add(rbtn);
                    placeholder.Controls.Add(new LiteralControl("<label for=\"" + rbtn.ID + "\">" + droption["QOM_Option"].ToString()
                        + "</label></div></div><div class=\"col-sm-3\"></div></div>"));

                }
                placeholder.Controls.Add(new LiteralControl("<div class=\"row\"><div class=\"col-sm-3  form-group pull-left\">"));
                Button prevbtn = new Button();
                prevbtn.ID = "btnprev" + questionNo.ToString();
                prevbtn.CommandName = "PrevView";
                prevbtn.Text = "« Prev";
                prevbtn.CssClass = "btn btn-default";
                if (questioncount != 1)
                {
                    placeholder.Controls.Add(prevbtn);
                }
                placeholder.Controls.Add(new LiteralControl(" </div><div class=\"col-sm-3  form-group  pull-right\">"));
                Button btnNext = new Button();
                btnNext.ID = "btnNext" + questionNo.ToString();
                btnNext.Text = "Next »";
                btnNext.CssClass = "btn btn-default";

                btnNext.ValidationGroup = questionNo.ToString();
                btnNext.Click += new EventHandler(btnNext_click);

                Button btnSubmit = new Button();
                btnSubmit.ID = "btnNext" + questionNo.ToString();
                btnSubmit.Text = "Submit";
                btnSubmit.CssClass = "btn btn-default";
                btnSubmit.ValidationGroup = questionNo.ToString();
                btnSubmit.Click += new EventHandler(btnSubmit_click);

                if (questioncount != dsGetquestionList.Tables[0].Rows.Count)
                {
                    placeholder.Controls.Add(btnNext);
                }
                if (questioncount == dsGetquestionList.Tables[0].Rows.Count)
                {
                    placeholder.Controls.Add(btnSubmit);
                }
                placeholder.Controls.Add(new LiteralControl("</div></div></div>"));
                View myView = new View();
                myView.ID = "View" + questionNo.ToString();
                myView.Controls.Add(placeholder);
                MultiView1.Views.Add(myView);
                MultiView1.ActiveViewIndex = 0;
                questioncount += 1;

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserName.Text = " " + userVo.UserName;
            if (!IsPostBack)
            {
                //string prevPage = Request.UrlReferrer.ToString();

            }
        }
        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                bool result = false;
                result = StoreAnswerToQuestion(sender);
                if (result)
                    Response.Redirect("Result.aspx");
            }
        }
        protected void btnNext_click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                bool result = false;
                result = StoreAnswerToQuestion(sender);
                if (result)
                    MultiView1.ActiveViewIndex += 1;
            }
        }
        private bool StoreAnswerToQuestion(object sender)
        {
            Button btn = (Button)sender;
            string btnId = btn.ID;
            bool result = false;
            foreach (Control d in MultiView1.Views[MultiView1.ActiveViewIndex].Controls)
            {

                if (d is PlaceHolder)
                {
                    foreach (Control c in d.Controls)
                    {
                        if (c is RadioButton)
                        {
                            RadioButton rb = (RadioButton)c;
                            if (rb.GroupName == btn.ValidationGroup && rb.Checked == true)
                            {
                                result = fpUserBo.SetAnswerToQuestion(userVo.UserId, Convert.ToInt32(btn.ValidationGroup), Convert.ToInt32(rb.ValidationGroup));
                            }
                        }
                    }
                }
            }
            return result;
        }
        protected void cvRadioButtonGroup_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool itemSelected = false;

            CustomValidator cv = (CustomValidator)source;
            foreach (Control c in form1.Controls)
            {
                if (c is MultiView)
                {
                    int viewIndex = MultiView1.ActiveViewIndex;
                    foreach (Control d in MultiView1.Views[viewIndex].Controls)
                    {

                        if (d is PlaceHolder)
                        {
                            foreach (Control e in d.Controls)
                            {
                                if (e is RadioButton)
                                {
                                    RadioButton rb = (RadioButton)e;
                                    if (rb.GroupName == cv.ValidationGroup && rb.Checked == true)
                                    {
                                        itemSelected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            args.IsValid = itemSelected;
        }
        protected void btnLogOut_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}
