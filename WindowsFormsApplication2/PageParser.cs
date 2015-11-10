using System;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using ElemParser;
using System.Text.RegularExpressions;

public class PageParser
{
    HtmlNode node;
    string fileName;

    public PageParser(HtmlNode _node)
    {
        node = _node;
    }

    public PageParser(HtmlNode _node, string _filename) : this(_node)
    {
        fileName = _filename;
    }

    public string FileName
    {
        get {return fileName; }
        set { fileName = value; }
    }

    public void saveToFile()
    {
        Encoding locEncoding = Encoding.Default;
        StreamWriter swriter = new StreamWriter(fileName, false, locEncoding);
        node.WriteTo(swriter);
        swriter.Close();     
    }

    public void saveNodeToFile(HtmlNode _node)
    {
        HtmlNode nodeContent;
        string stNodeContent;
        Encoding locEncoding = Encoding.Default;
        StreamWriter swriter = new StreamWriter(fileName, true, locEncoding);

        _node.WriteTo(swriter);
        swriter.Close();
    }

    public void traverse(string _desc, HtmlNode _node)
    {
        HtmlAgilityPack.HtmlNodeCollection nodes;
        CharacterNode chNode;
        nodes = _node.SelectNodes(@"//table[@class='ulist mt10']/tbody/tr");

        foreach (HtmlNode chnode in nodes)
        {
            saveNodeToFile(chnode);
            chNode = new CharacterNode(chnode);            
        }        
    }

    private void parseUserNode(InfoNode _caller, HtmlNode _localNode)
    {
        ;
        HtmlNodeCollection coll;
        ;
        saveNodeToFile(node);

        coll = _localNode.SelectNodes(_localNode.XPath + @"/a/span[@class]");  //user online
        foreach (HtmlNode localSubNode in coll)
        {
            parseNode(_caller, localSubNode);

        }
    }

    private string cleanupHTMLTags(string _inputString)
    {
        string ret;         
        ;
        ret = _inputString;
        ret = Regex.Replace(ret, @"<[^>]+>|&nbsp;", "").Trim();
        ret = Regex.Replace(ret, @"\s{1,}", "");
        //Console.OutputEncoding = Encoding.Default;

        byte[] bytes = Encoding.UTF8.GetBytes(ret);
        char[] asciiChars = new char[Encoding.UTF8.GetCharCount(bytes, 0, bytes.Length)];
        //Encoding.UTF8.GetChars(bytes, 0, bytes.Length, asciiChars, 0);
        string asciiString = new string(asciiChars);

        Console.WriteLine(asciiString);
        return ret;
    }

    private string cleanNodeInnerText(HtmlNode _node)
    {
        string ret;
        ;
        ret = cleanupHTMLTags(_node.InnerText);
        return ret;
    }

    public void parseNode(InfoNode _caller, HtmlNode _node)
    {
        string _str;

        if (_node.HasAttributes)
        {
            foreach (HtmlAttribute attr in _node.Attributes)
            {
                switch (attr.Value)
                {
                    case "user":
                        parseUserNode(_caller, _node);
                        break;
                    case "":
                        _caller.Online = (bool)true;
                        _caller.Name = cleanNodeInnerText(_node);
                        break;
                    case "c_66":
                        _caller.Online = (bool)false;
                        _caller.Name = cleanNodeInnerText(_node);                        
                        break;
                    case "c_99":
                        _str    = cleanNodeInnerText(_node);
                        _caller.XP = Convert.ToInt64(_str);
                        break;
                    case "c_66 ml21":
                        _caller.Title = cleanNodeInnerText(_node);
                        break;
                    default:
                        break;                       
                }
            }
        }
    }

}

