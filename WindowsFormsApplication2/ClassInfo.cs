using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WindowsFormsApplication2
{
    public class InfoNode
    {
        protected int internalId;
        protected int deckPower;
        protected string name;
        protected string title;
        protected int experience;
        protected int level;

        public InfoNode(int _id, string _name)
        {
            internalId = _id;
            name = _name;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public InfoNode(HtmlNode _node)
        {
            
        }
        public string Details()
        {
            string ret;

            ret = string.Format("Id = {0}; Name = {1}", internalId, name);

            return ret;
        }
    }
}
