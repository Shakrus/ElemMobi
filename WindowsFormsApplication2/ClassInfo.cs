using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ElemParser
{
    public class InfoNode
    {
        protected int internalId;
        protected int deckPower;
        protected string name;
        protected string title;
        protected long experience;
        protected int level;
        protected bool isOnline;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public InfoNode(int _id, string _name)
        {
            internalId = _id;
            name = _name;
        }
        public bool Online
        {
            get { return isOnline; }
            set { isOnline = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public long XP
        {
            get { return experience; }
            set { experience = value; }
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
