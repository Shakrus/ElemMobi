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
using System.Configuration;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        string sURL;
        Connection connect = new Connection();
        WebReq webReq;
        private string login;
        private string pass;
        bool silentLogin;

        public Form1()
        {
            InitializeComponent();

            loadSettingsFromFile();

            Login();            
        }

        private void loadSettingsFromFile()
        {
            var appSettings = ConfigurationManager.AppSettings;

            if (appSettings["login"] != null)
            {
                login = appSettings["login"];
            }
            if (appSettings["password"] != null)
            {
                pass = appSettings["password"];
            }
            if (appSettings["siteName"] != null)
            {
                sURL = appSettings["siteName"];
            }
            if (appSettings["silentLogin"] != null)
            {
                silentLogin = Convert.ToBoolean(appSettings["silentLogin"]);
            }
        }


        private static void outputWebRespConsole(HttpWebResponse WebResp)
        {
            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);
        }  

        private void Login()
        {
            
            string postPassword;
            postPassword = "plogin=" + login + "&ppass=" + pass;

            webReq = new WebReq(sURL, getPost.post, connect, silentLogin, postPassword);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            webReq = new WebReq(sURL+ tbPageName.Text, getPost.get, connect, false);
            webReq.doParse();
        }

    }
}
