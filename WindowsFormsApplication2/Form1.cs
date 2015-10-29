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
using HtmlAgilityPack;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        string sURL;
        CookieContainer cookieContainer;
        HttpWebRequest glbWebReq;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void addCookie(HttpWebRequest   _mywebRequest)
        {
            IWebProxy proxy = _mywebRequest.Proxy;

            if (proxy != null)
            {
                string proxyuri = proxy.GetProxy(_mywebRequest.RequestUri).ToString();
                _mywebRequest.UseDefaultCredentials = true;
                _mywebRequest.Proxy = new WebProxy(proxyuri, false);
                _mywebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }

            _mywebRequest.CookieContainer = cookieContainer;

        }
        
        private void initCookieContrainer()
        {
            cookieContainer = new CookieContainer();            
        }

        private void GetResponse(HttpWebRequest _webRequest)
        {

        }

        private void parseAnswer(Stream _stream)
        {
            HtmlAgilityPack.HtmlDocument document;            
            HtmlNode node;

            StreamReader _Answer = new StreamReader(_stream);

            document = new HtmlAgilityPack.HtmlDocument();
            document.Load(_stream);
            
            node = document.DocumentNode;
            Console.WriteLine(node.WriteTo());
            //Console.WriteLine(_Answer.ReadToEnd());
        }

        private Stream start_get(HttpWebRequest _WebReq, String _str)
        {
            //Our getVars, to test the get of our php. 
            //We can get a page without any of these vars too though.
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
            PutConsole(WebResp);
            cookieContainer.Add(WebResp.Cookies);

            //Now, we read the response (the string), and output it.
            Stream Answer = WebResp.GetResponseStream();

            parseAnswer(Answer);
            WebResp.Close();
            return Answer;
        }

        private static void PutConsole(HttpWebResponse WebResp)
        {
            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);
        }  

        private Stream start_post(HttpWebRequest _WebReq, string strPage, string strBuffer)
        {
            //Our postvars
            byte[] buffer = Encoding.ASCII.GetBytes(strBuffer);
       
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
            
            cookieContainer.Add(WebResp.Cookies);
            
            WebResp.Close();
            return Answer;
        }

        private Stream Login(HttpWebRequest _webReq)
        {
            string postPassword;
            postPassword = "plogin=" + tbLogin.Text + "&ppass=" + tbPass.Text;

            return start_post(_webReq, sURL, postPassword);
        }

        private void InitWebReq(string strPage)
        {
            glbWebReq = (HttpWebRequest)WebRequest.Create(strPage);
            addCookie(glbWebReq);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream Answer;
            
            sURL = "http://elem.mobi/";

            initCookieContrainer();
            InitWebReq(sURL);

            //start_get(glbWebReq, "");

            Answer  =   Login(glbWebReq);            
                        
            //Answer = start_get(glbWebReq, "guild/");

            //Answer = start_get(glbWebReq, "guild/page_2");

            //Answer = start_get(glbWebReq, "user/1408977/");
        }
    }
}
