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
using System.Text.RegularExpressions;


namespace ElemParser
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

        string makeURL(string _st)
        {
            string ret;
            ret = Regex.Replace(_st, @"\/", "!");
            ret = Regex.Replace(ret, ":", ".");

            return ret;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CharacterNode node;
            string fileName;
            node = new CharacterNode(1, "test");
            Console.WriteLine(node.Details());

            fileName = makeURL(tbPageName.Text);
            fileName = string.Format(@"d:\temp\{0}.txt", fileName);

            for (int i = 1; i <= 5; i++)
            {
                webReq = new WebReq(string.Format("{0}{1}/page_{2}", sURL, tbPageName.Text, i), getPost.get, connect, false);
                webReq.doParse(fileName);

            }
        }

    }
}
