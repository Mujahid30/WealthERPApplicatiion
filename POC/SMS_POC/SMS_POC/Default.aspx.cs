using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;


namespace SMS_POC
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                String sendToPhoneNumber = txtPhone.Text.ToString();
                String userid = "2000028587";
                String passwd = "KyhdQmEdD";
                String message = txtMessage.Text.ToString();
                String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" + sendToPhoneNumber + "&msg=" + message + "&userid=" + userid +
                "&password=" + passwd + "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN";
                request = WebRequest.Create(url);
                //in case u work behind proxy, uncomment the commented code and provide correct
                //details
                /*WebProxy proxy = new WebProxy("http://proxy:80/", true);
                proxy.Credentials = new NetworkCredential("userId", "password", "Domain");
                request.Proxy = proxy;
                */
                // Send the 'HttpWebRequest' and wait for response.
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                lblMessage.Text = result.ToString();
                lblMessage.Visible = true;
                //Console.WriteLine(result);
                reader.Close();
                stream.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
    }
}
