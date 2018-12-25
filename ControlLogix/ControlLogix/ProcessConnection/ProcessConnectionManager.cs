using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPA_T3000.Process_Connection;

namespace ControlLogix.ProcessConnection
{
    public class ProcessConnectionManager
    {
        static public LogicBlock GetLogicBlock(int id)
        {
            return ProcessConnectionTCPClient.RequestComponent(id.ToString(), 12345);
        }

        static public LogicBlock[] GetLogicBlocks()
        {
            return ProcessConnectionTCPClient.RequestComponents(12345);
        }
    }
}
