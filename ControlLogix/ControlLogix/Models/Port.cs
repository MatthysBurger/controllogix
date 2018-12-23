using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlLogix.Models
{
    public class Port
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Input { get; set; }
        public string Value { get; set; }
        public bool IsArchived { get; set; }
        public bool IsConnectable { get; set; }
    }
}
