using System;
using System.IO;
using System.Text;
using HtmlAgilityPack;

public class PageParser
{
    HtmlNode node;

    public PageParser(HtmlNode  _node)
	{
        node = _node;
	}

    public void saveToFile(string _fileName)
    {
        Encoding locEncoding = Encoding.Default;
        StreamWriter swriter = new StreamWriter(_fileName, false, locEncoding);
        node.WriteTo(swriter);
        swriter.Close();     
    }

    public void traverse(string _desc, HtmlNode _node)
    {
        
        foreach (HtmlNode chnode in _node.ChildNodes)
        {
            traverse(_desc + _node.Id, chnode);
        }        
    }
}

