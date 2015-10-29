using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        string sURL;
        WebReq webReq;

        public Form1()
        {
            sURL = "http://elem.mobi/";
            
            InitializeComponent();

            Login();            
        }
        
        
        private void start_get(HttpWebRequest _WebReq, String _str)
        {
            //Our getVars, to test the get of our php. 
            //We can get a page without any of these vars too though.
            /*
            string getVars = _str;

            //Initialization, we use localhost, change if applicable
            _WebReq = (HttpWebRequest)WebRequest.Create
                (string.Format("{0}{1}", 
                    sURL,
                    getVars));            
            //This time, our method is GET.
            addCookie(_WebReq);
            _WebReq.Method = "GET";

            //From here on, it's all the same as above.
            HttpWebResponse WebResp = (HttpWebResponse)_WebReq.GetResponse();
            //Let's show some information about the response
            outputWebRespConsole(WebResp);
            formConnection.updateCookies(WebResp);
            
            //Now, we read the response (the string), and output it.
            Stream Answer = WebResp.GetResponseStream();

            parseAnswer(Answer);
            WebResp.Close();
            return Answer;
            */
        }

        private static void outputWebRespConsole(HttpWebResponse WebResp)
        {
            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);
        }  

        private void start_post(string strPage, string strBuffer)
        {
            /*
            //Our postvars
            webReq = new WebReq(strPage, getPost.post, strBuffer);

            //Our method is post, otherwise the buffer (postvars) would be useless
            _WebReq.Method = "POST";
            //We use form contentType, for the postvars.
            _WebReq.ContentType = "application/x-www-form-urlencoded";
            //The length of the buffer (postvars) is used as contentlength.
            _WebReq.ContentLength = buffer.Length;
            //We open a stream for writing the postvars
            Stream PostData = _WebReq.GetRequestStream();
            //Now we write, and afterwards, we close. Closing is always important!
            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
 //Get the response handle, we have no true response yet!
            HttpWebResponse WebResp = (HttpWebResponse)_WebReq.GetResponse();
            
            //Let's show some information about the response
            //Console.WriteLine(WebResp.StatusCode);
            //Console.WriteLine(WebResp.Server);

            //Now, we read the response (the string), and output it.
            Stream Answer = WebResp.GetResponseStream();
            parseAnswer(Answer);
                        

            formConnection.updateCookies(WebResp);            
            
            WebResp.Close();
            return Answer;
             */            
        }

        private void Login()
        {
            
            string postPassword;
            postPassword = "plogin=" + tbLogin.Text + "&ppass=" + tbPass.Text;

            webReq = new WebReq(sURL, getPost.post, true, postPassword);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //start_get(glbWebReq, "");

                        
            webReq = new WebReq(sURL+"guild/", getPost.get, false);

            //Answer = start_get(glbWebReq, "guild/page_2");

            //Answer = start_get(glbWebReq, "user/1408977/");
        }
    }
}
