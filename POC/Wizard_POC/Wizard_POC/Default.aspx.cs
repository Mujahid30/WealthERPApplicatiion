using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using Microsoft.FSharp;



namespace Wizard_POC
{
    public partial class _Default : System.Web.UI.Page
    {
        RiskOptionVo riskOptionVo = new RiskOptionVo();
        List<RiskOptionVo> listRiskOptionVo = new List<RiskOptionVo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblDate.Text = DateTime.Today.ToLongDateString();
                string xmlPath = Server.MapPath("\\XML\\Questions.xml");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode xmlNode = xmlDoc.DocumentElement;
                int count = 1;
                listRiskOptionVo = new List<RiskOptionVo>();
                foreach (XmlNode tempNode in xmlNode.ChildNodes)
                {
                    if (tempNode.Name.ToLower() == "rpquestion")
                    {
                        switch (count)
                        {
                            #region Q1
                            case 1:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ1.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ1A1.Visible = true;
                                        rbtnQ1A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q1A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ1A2.Visible = true;
                                        rbtnQ1A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q1A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ1A3.Visible = true;
                                        rbtnQ1A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q1A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ1A4.Visible = true;
                                        rbtnQ1A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q1A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ1A5.Visible = true;
                                        rbtnQ1A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q1A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ1A6.Visible = true;
                                        rbtnQ1A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q1A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }

                                }
                                break;
                            #endregion Q1

                            #region Q2
                            case 2:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ2.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ2A1.Visible = true;
                                        rbtnQ2A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q2A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ2A2.Visible = true;
                                        rbtnQ2A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q2A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ2A3.Visible = true;
                                        rbtnQ2A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q2A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ2A4.Visible = true;
                                        rbtnQ2A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q2A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ2A5.Visible = true;
                                        rbtnQ2A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q2A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ2A6.Visible = true;
                                        rbtnQ2A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q2A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                }
                                break;
                            #endregion Q2

                            #region Q3
                            case 3:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ3.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ3A1.Visible = true;
                                        rbtnQ3A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q3A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ3A2.Visible = true;
                                        rbtnQ3A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q3A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ3A3.Visible = true;
                                        rbtnQ3A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q3A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ3A4.Visible = true;
                                        rbtnQ3A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q3A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ3A5.Visible = true;
                                        rbtnQ3A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q3A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ3A6.Visible = true;
                                        rbtnQ3A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q3A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }

                                }
                                break;
                            #endregion Q3

                            #region Q4
                            case 4:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ4.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ4A1.Visible = true;
                                        rbtnQ4A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q4A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ4A2.Visible = true;
                                        rbtnQ4A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q4A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ4A3.Visible = true;
                                        rbtnQ4A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q4A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ4A4.Visible = true;
                                        rbtnQ4A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q4A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ4A5.Visible = true;
                                        rbtnQ4A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q4A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ4A6.Visible = true;
                                        rbtnQ4A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q4A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }


                                }
                                break;
                            #endregion Q4

                            #region Q5
                            case 5:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ5.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ5A1.Visible = true;
                                        rbtnQ5A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q5A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ5A2.Visible = true;
                                        rbtnQ5A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q5A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ5A3.Visible = true;
                                        rbtnQ5A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q5A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ5A4.Visible = true;
                                        rbtnQ5A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q5A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ5A5.Visible = true;
                                        rbtnQ5A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q5A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ5A6.Visible = true;
                                        rbtnQ5A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q5A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }


                                }
                                break;
                            #endregion Q5

                            #region Q6
                            case 6:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ6.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ6A1.Visible = true;
                                        rbtnQ6A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q6A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ6A2.Visible = true;
                                        rbtnQ6A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q6A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ6A3.Visible = true;
                                        rbtnQ6A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q6A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ6A4.Visible = true;
                                        rbtnQ6A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q6A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ6A5.Visible = true;
                                        rbtnQ6A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q6A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ6A6.Visible = true;
                                        rbtnQ6A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q6A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }


                                }
                                break;
                            #endregion Q6

                            #region Q7
                            case 7:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ7.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ7A1.Visible = true;
                                        rbtnQ7A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q7A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ7A2.Visible = true;
                                        rbtnQ7A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q7A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ7A3.Visible = true;
                                        rbtnQ7A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q7A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ7A4.Visible = true;
                                        rbtnQ7A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q7A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ7A5.Visible = true;
                                        rbtnQ7A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q7A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ7A6.Visible = true;
                                        rbtnQ7A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q7A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }


                                }
                                break;
#endregion Q7

                            #region Q8
                            case 8:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ8.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ8A1.Visible = true;
                                        rbtnQ8A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q8A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ8A2.Visible = true;
                                        rbtnQ8A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q8A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ8A3.Visible = true;
                                        rbtnQ8A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q8A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ8A4.Visible = true;
                                        rbtnQ8A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q8A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ8A5.Visible = true;
                                        rbtnQ8A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q8A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ8A6.Visible = true;
                                        rbtnQ8A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q8A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }


                                }
                                break;
                                #endregion Q8

                            #region Q9
                            case 9:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ9.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ9A1.Visible = true;
                                        rbtnQ9A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q9A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ9A2.Visible = true;
                                        rbtnQ9A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q9A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ9A3.Visible = true;
                                        rbtnQ9A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q9A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ9A4.Visible = true;
                                        rbtnQ9A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q9A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ9A5.Visible = true;
                                        rbtnQ9A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q9A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ9A6.Visible = true;
                                        rbtnQ9A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q9A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }



                                }
                                break;
                            #endregion Q9

                            #region Q10
                            case 10:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ10.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ10A1.Visible = true;
                                        rbtnQ10A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q10A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ10A2.Visible = true;
                                        rbtnQ10A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q10A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ10A3.Visible = true;
                                        rbtnQ10A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q10A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ10A4.Visible = true;
                                        rbtnQ10A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q10A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ10A5.Visible = true;
                                        rbtnQ10A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q10A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ10A6.Visible = true;
                                        rbtnQ10A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q10A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }



                                }
                                break;
                                #endregion Q10

                            #region Q11
                            case 11:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ11.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ11A1.Visible = true;
                                        rbtnQ11A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q11A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ11A2.Visible = true;
                                        rbtnQ11A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q11A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ11A3.Visible = true;
                                        rbtnQ11A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q11A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ11A4.Visible = true;
                                        rbtnQ11A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q11A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ11A5.Visible = true;
                                        rbtnQ11A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q11A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ11A6.Visible = true;
                                        rbtnQ11A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q11A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }


                                }
                                break;
                            #endregion Q11

                            #region Q12
                            case 12:
                                foreach (XmlNode innerNode in tempNode.ChildNodes)
                                {
                                    if (innerNode.Name.ToLower() == "question")
                                        lblQ12.Text = innerNode.InnerText.Trim();
                                    else if (innerNode.Name.ToLower() == "a")
                                    {
                                        rbtnQ12A1.Visible = true;
                                        rbtnQ12A1.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q12A1";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "b")
                                    {
                                        rbtnQ12A2.Visible = true;
                                        rbtnQ12A2.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q12A2";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "c")
                                    {
                                        rbtnQ12A3.Visible = true;
                                        rbtnQ12A3.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q12A3";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "d")
                                    {
                                        rbtnQ12A4.Visible = true;
                                        rbtnQ12A4.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q12A4";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "e")
                                    {
                                        rbtnQ12A5.Visible = true;
                                        rbtnQ12A5.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q12A5";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }
                                    else if (innerNode.Name.ToLower() == "f")
                                    {
                                        rbtnQ12A6.Visible = true;
                                        rbtnQ12A6.Text = ((innerNode.InnerText.Trim()).Split('|'))[0];
                                        riskOptionVo = new RiskOptionVo();
                                        riskOptionVo.Option = "Q12A6";
                                        riskOptionVo.Value = int.Parse(((innerNode.InnerText.Trim()).Split('|'))[1]);
                                        listRiskOptionVo.Add(riskOptionVo);
                                    }


                                }
                                break;
                            #endregion Q12

                        }
                        count = count + 1;
                    }

                }
                ViewState["ListRiskOption"] = listRiskOptionVo;
                

            }
        }
        protected void btnSubmitRisk_Click(object sender, EventArgs e)
        {
            string tempRID = "";
            int rScore = 0;
            listRiskOptionVo = (List<RiskOptionVo>) ViewState["ListRiskOption"];
            for (int i = 1; i <= 12; i++)
            {
                for(int j=1;j<=6;j++)
                {
                    tempRID = "rbtnQ" + i + "A" + j;

                    RadioButton rbtnQAns = (RadioButton)wFinancialPlanning.FindControl(tempRID);
                    if (rbtnQAns!=null && rbtnQAns.Checked)
                    {
                        for(int rCount=0;rCount<=listRiskOptionVo.Count;rCount++)
                        {
                            if (listRiskOptionVo[rCount].Option == "Q" + i + "A" + j)
                            {
                                rScore = rScore + listRiskOptionVo[rCount].Value;
                                break;
                            }
                        }
                    }
                }
            }
            lblRScore.Visible = true;
            lblRClass.Visible = true;
            lblRScore.Text = rScore.ToString();
            if (rScore > 90)
                lblRClass.Text = "Aggressive";
            else if (rScore > 50)
                lblRClass.Text = "Moderate";
            else
                lblRClass.Text = "Conservative";
        }
        protected void ddlDependent_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void ddlGoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            divCalculator.Visible = true;
            tblCalculator.Visible = true;
            tblOutPut.Visible = false;
            switch (ddlGoal.SelectedValue.ToString())
            {
                case "Home":
                    ddlDependent.Visible = false;
                    lblObjective.Text = "Objective: Home";
                    lblCostToday.Text = "Home Cost Today:";
                    txtCostToday.Text = "";
                    txtCurrentValue.Text = "";
                    txtRateInterest.Text = "";
                    txtRequiredAfter.Text = "";
                    txtRequiredRate.Text = "";

                    break;
                case "Education":
                    ddlDependent.Visible = true;
                    lblObjective.Text = "Objective: Education Of";
                    lblCostToday.Text = "Education Cost Today:";
                    txtCostToday.Text = "";
                    txtCurrentValue.Text = "";
                    txtRateInterest.Text = "";
                    txtRequiredAfter.Text = "";
                    txtRequiredRate.Text = "";
                    break;
                case "Marriage":
                    ddlDependent.Visible = true;
                    lblObjective.Text = "Objective: Marriage Of";
                    lblCostToday.Text = "Mariage Cost Today:";
                    txtCostToday.Text = "";
                    txtCurrentValue.Text = "";
                    txtRateInterest.Text = "";
                    txtRequiredAfter.Text = "";
                    txtRequiredRate.Text = "";
                    break;
                default:

                    break;
            }
            
        }
        /// <summary>/// Calculates the present value of a loan based upon constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">The interest rate.</param>
        /// <param name="nper">Total number of payments.</param>
        /// <param name="pmt">payment made each period</param>
        /// <param name="fv">Future value.</param>
        /// <param name="type">Indicates when payments are due. 0 = end of period, 1 = beginning of period.</param>
        /// <returns>The Present Value</returns>
        public static double PV(double rate, double nper, double pmt, double fv, double type)
        {
            if (rate == 0.0)
                return (-fv - (pmt * nper));
            else
                return (pmt * (1.0 + rate * type) * (1.0 - Math.Pow((1.0 + rate), nper)) / rate - fv) / Math.Pow((1.0 + rate), nper);
        }
        public double FutureValue(double rate,double nper,double pmt,double pv,int type)
        {
            double result=0;
            result = System.Numeric.Financial.Fv(rate, nper, pmt, pv, 0);
            return result;
        }
        public double PMT(double rate, double nper, double pv, double fv, int type)
        {
            double result = 0;
            result = System.Numeric.Financial.Pmt(rate, nper, pv, fv, 0);
            return result;
        }
        protected void btnCalSubmit_Click(object sender, EventArgs e)
            {
            double futureInvValue = 0;
            double futureCost = 0;
            double requiredSavings = 0;
            double costToday = 0;
            double requiredAfter = 0;
            double currentValue = 0;
            double rateEarned = 0;
            double rateOfReturn = 0;
            string goal=ddlGoal.SelectedValue.ToString();
            costToday = double.Parse(txtCostToday.Text);
            requiredAfter = double.Parse(txtRequiredAfter.Text);
            currentValue = double.Parse(txtCurrentValue.Text);
            rateEarned = (double.Parse(txtRateInterest.Text))/12;
            rateOfReturn = (double.Parse(txtRequiredRate.Text))/12;

            futureCost = Math.Abs(FutureValue(0.06,requiredAfter,0,costToday,0));
            futureInvValue=Math.Abs(FutureValue(rateEarned,requiredAfter,0,currentValue,0));
            requiredSavings=Math.Abs(PMT((rateOfReturn/12),(requiredAfter*12),0,(futureCost-futureInvValue),0));

            tblOutPut.Visible=true;
            switch(goal)
            {
                case "Home":
                    lblCost.Text="Home Cost After "+requiredAfter+" years:";
                    break;
                case "Education":
                    lblCost.Text="Education Cost After "+requiredAfter+" years:";
                    break;
                case "Marriage":
                    lblCost.Text="Marriage Cost After "+requiredAfter+" years:";
                    break;
            }
            lblValue.Text="Value of Current Investments after "+requiredAfter+" Years";

            lblValueResult.Text = futureInvValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            lblCostResult.Text = futureCost.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            lblSavingsResult.Text = requiredSavings.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

        }
    }
}
