using System;
using System.Net;
using System.IO;
using System.Text;
using HtmlAgilityPack;

public class Connection
{
    private CookieContainer localCookie;
    private HttpWebRequest glbWebReq;

    public  CookieContainer cookies
    {
        get { return localCookie; }        
    }    
    public void updateCookies(HttpWebResponse _webResponse)
    {
        localCookie.Add(_webResponse.Cookies);
    }
	public void initCookies()
	{
        localCookie = new CookieContainer();
	}
    public Connection()
    {
        initCookies();        
    }
}
public enum getPost : byte
{
    get = 0,
    post = 1
}

public class WebReq
{
    Connection formConnection = new Connection();

    HttpWebRequest webRequestPOST;
    HttpWebRequest webRequestGET;

    HttpWebResponse webResponse;
    getPost currentRequest;
    public Stream Answer;

    public string hostSiteName;    

    public WebReq(string _siteName, getPost _opType) : this(_siteName, _opType, true, "")
    { 
    }

    public WebReq(string _siteName, getPost _opType, bool _silent = true, string _strBuffer = "")
    {
        HttpWebRequest localRequest;
        string requestMethod;

        hostSiteName = _siteName;
        currentRequest = _opType;

        switch (currentRequest)
        {
            case getPost.get:
                webRequestGET = (HttpWebRequest)WebRequest.Create(_siteName);
                requestMethod = "GET";
                localRequest = webRequestGET;
                break;
            case getPost.post:
                webRequestPOST = (HttpWebRequest)WebRequest.Create(_siteName);
                requestMethod = "POST";
                localRequest = webRequestPOST;
                break;
            default:
                localRequest = null;
                requestMethod = "";
                break;
        }
        localRequest.ContentType = "application/x-www-form-urlencoded";
        localRequest.Method = requestMethod;
        addCookie(localRequest, formConnection);

        if (_strBuffer.Length != 0 && currentRequest == getPost.post)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(_strBuffer);
            localRequest.ContentLength = buffer.Length;
            Stream PostData = localRequest.GetRequestStream();
            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
        }
        webResponse = (HttpWebResponse)localRequest.GetResponse();
        Answer = webResponse.GetResponseStream();

        if (!_silent)
        {
            parseAnswer(Answer);
        }
        formConnection.updateCookies(webResponse);
        webResponse.Close();
    }

    private void addCookie(HttpWebRequest _webRequest, Connection   _connection)
    {
        IWebProxy proxy = _webRequest.Proxy;

        if (proxy != null)
        {
            string proxyuri = proxy.GetProxy(_webRequest.RequestUri).ToString();
            _webRequest.UseDefaultCredentials = true;
            _webRequest.Proxy = new WebProxy(proxyuri, false);
            _webRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        _webRequest.CookieContainer = _connection.cookies;
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
    }

}
