using System;
using System.Collections.Generic;
using System.Text;

namespace Virtualcompanion.Core.Contexts.Features
{
    [Flags]
    public enum VirtualCompanionExecutionContextFeatureType
    {
        None =      0,
        Input =     1 << 0, 
        Output =    1 << 1,
        Audio =     1 << 2, 
        Text =      1 << 3
    }
}
