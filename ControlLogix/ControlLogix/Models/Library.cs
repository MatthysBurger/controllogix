using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlLogix.Models
{
    public class Library
    {
        public int PrimaryKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private Dictionary<int, ControlBlock> _controlblocks = new Dictionary<int, ControlBlock>();
        public Dictionary<int, ControlBlock> ControlBlocks
        {
            get
            {
                return _controlblocks;
            }
        }
    }
}
