using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElemParser
{
    public class GuildNode : InfoNode
    {
        CharacterNode[] characters;
        public GuildNode(int _id, string _name) : base(_id, _name)
        {
        }
    }
}
