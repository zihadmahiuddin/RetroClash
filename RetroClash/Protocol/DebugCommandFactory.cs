using System;
using System.Collections.Generic;

namespace RetroClash.Protocol
{
    public class DebugCommandFactory
    {
        public static Dictionary<int, Type> DebugCommands;

        public DebugCommandFactory()
        {
            DebugCommands = new Dictionary<int, Type>();
        }
    }
}