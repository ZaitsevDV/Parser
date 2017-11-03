using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Entry
    {
        static public uint Id { set; get; } = 0;
        public DateTime DateTime { set; get; }
        public string Message { set; get; }
        public string SenderName { set; get; }
        public string Type { set; get; }
        public uint SenderID { set; get; } = 0;
        public uint RecipientID { set; get; } = 0;
        
    }
}


