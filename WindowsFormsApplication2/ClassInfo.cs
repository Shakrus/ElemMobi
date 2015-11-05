using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class InfoNode
    {
        protected int internalId;
        protected string name;
        protected string title;
        protected int experience;
        protected int level;

        public InfoNode(int _id, string _name)
        {
            internalId = _id;
            name = _name;
        }

    }
}
