using System;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using WindowsFormsApplication2;

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
        //nodeContent = _node.SelectSingleNode(@"td[@class='num']");
        //stNodeContent   =   nodeContent.InnerText;
        //nodeContent.WriteTo(swriter);
        node.WriteTo(swriter);
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
            //Console.WriteLine(String.Format("{0} {1}", chnode.InnerText, chnode.Attributes));
        }        
    }

    private void parseUserNode(InfoNode _caller)
    {
        HtmlNode localNode;
        HtmlNodeCollection coll;
        ;
        saveNodeToFile(node);

        coll = node.SelectNodes("attribute::class");  //user online
        //coll = node.SelectNodes(node.XPath + "/span");  //user online
        foreach (HtmlNode _localNode in coll)
        {

        }

        

        localNode = node.SelectSingleNode(@"span[@class]");  //user online
        if (localNode == null)
        {
            localNode = node.SelectSingleNode(@"span[@class]"); //user offline
        }
        _caller.Name = localNode.InnerText;


        foreach (HtmlNode _localNode in node.ChildNodes)
        {
            switch (_localNode.Name)
            {
                case "span":
                    break;
                default:
                    break;
            }

        }        
    }

    public void parseNode(InfoNode _caller)
    {
        int i;
        if (node.HasAttributes)
        {
            foreach (HtmlAttribute attr in node.Attributes)
            {
                switch (attr.Value)
                {
                    case "user":
                        //node = node.ChildNodes[1];
                        parseUserNode(_caller);
                        break;
                    default:
                        break;                       
                }
            }
        }
    }

}

