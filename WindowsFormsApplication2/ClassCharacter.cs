using System;
using HtmlAgilityPack;
using WindowsFormsApplication2;

namespace WindowsFormsApplication2
{
    public class CharacterNode : InfoNode
    {
        //для персонажа достаточно айди и имени
        public CharacterNode(int _id, string _name) : base(_id, _name)
        {
        }
/*<tr class="odd2">
                                <td class="num">7</td>
                                <td class="ava"><img src="/cards/fire/fire_servant_1_100.jpg" height="25" width="25" alt=""></td>
                                <td class="user">
                                    <a  class="tdn" href="/user/324875/">
                                        <img class="mr5" src="/img/lg/lg-red-1-pvp.png" width="16" height="16" alt="">
                                        <span class="c_66">Аккэлия</span>
                                        <span class="c_99"> &nbsp; 4 269 183 371</span><br/>
                                        <span class="c_66 ml21">Маршал</span>
                                        <span class="fr c_99">163 д</span>
                                    </a>
                                </td>
                            </tr>
*/
        public CharacterNode(HtmlNode _node):base(_node)
        {
            HtmlNodeCollection collection;
            string strValue;
            string attrValue;
            HtmlNode characterNode;
            HtmlNodeCollection characterPropertiesCollection;
            int num;
            PageParser parser;

            collection = _node.SelectNodes(@"td");

            foreach (HtmlNode locNode in collection)
            {
                strValue = locNode.InnerText;
                if (locNode.Attributes.Count > 0 )
                {
                    attrValue = locNode.Attributes[0].Value;
                    switch (attrValue)
                    {
                        case "num":
                            internalId = Convert.ToInt32(strValue);
                            break;
                        case "ava":
                            break;
                        case "user":
                            parser = new PageParser(locNode, @"d:\temp\char.txt");
                            parser.parseNode(this);
                                                        
                            break;
                        default:
                            break;
                    }
                }
            }
            
        }
    }
}
